using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Service responsible for secure PIN management and validation.
    /// Provides secure storage and validation of PINs using SHA-256 hashing.
    /// </summary>
    public class PinService : IPinService
    {
        private readonly string _pinFilePath;
        private const string DEFAULT_PIN = "1234"; // Default PIN for first-time setup

        /// <summary>
        /// Initializes a new instance of the PinService.
        /// Sets up the file path for PIN storage in the user's application data folder.
        /// </summary>
        /// <remarks>
        /// The PIN file is stored in the user's ApplicationData folder under
        /// "MachineManagementSystem/pin.dat" for security and persistence.
        /// </remarks>
        public PinService()
        {
            _pinFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                "MachineManagementSystem", "pin.dat");
        }

        /// <summary>
        /// Validates a PIN against the stored hash asynchronously.
        /// This method compares the provided PIN with the stored hash using SHA-256.
        /// </summary>
        /// <param name="pin">The PIN to validate.</param>
        /// <returns>True if the PIN is valid; otherwise, false.</returns>
        /// <remarks>
        /// If no PIN file exists, the method falls back to the default PIN (1234).
        /// Empty or null PINs are automatically rejected.
        /// The PIN is hashed using SHA-256 before comparison for security.
        /// </remarks>
        public async Task<bool> ValidatePinAsync(string pin)
        {
            if (string.IsNullOrEmpty(pin))
                return false;

            // Check if PIN file exists, if not, use default PIN
            if (!File.Exists(_pinFilePath))
            {
                return pin == DEFAULT_PIN;
            }

            try
            {
                string storedHash = await File.ReadAllTextAsync(_pinFilePath);
                string inputHash = HashPin(pin);
                return storedHash == inputHash;
            }
            catch
            {
                // If there's an error reading the file, fall back to default PIN
                return pin == DEFAULT_PIN;
            }
        }

        /// <summary>
        /// Changes the current PIN to a new PIN asynchronously.
        /// This method validates the current PIN and then stores the new PIN hash.
        /// </summary>
        /// <param name="currentPin">The current PIN for verification.</param>
        /// <param name="newPin">The new PIN to set.</param>
        /// <returns>True if the PIN was successfully changed; otherwise, false.</returns>
        /// <remarks>
        /// The method performs the following validations:
        /// - New PIN must not be null or empty
        /// - New PIN must be at least 4 characters long
        /// - Current PIN must be valid
        /// The new PIN is hashed using SHA-256 before storage.
        /// </remarks>
        public async Task<bool> ChangePinAsync(string currentPin, string newPin)
        {
            if (string.IsNullOrEmpty(newPin) || newPin.Length < 4)
                return false;

            if (!await ValidatePinAsync(currentPin))
                return false;

            try
            {
                string newHash = HashPin(newPin);
                await EnsureDirectoryExists();
                await File.WriteAllTextAsync(_pinFilePath, newHash);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a PIN has been set in the system asynchronously.
        /// This method determines if a PIN file exists, indicating a PIN has been set.
        /// </summary>
        /// <returns>True if a PIN is set (PIN file exists); otherwise, false.</returns>
        /// <remarks>
        /// Used during application startup to determine if the user should be prompted
        /// to set an initial PIN or proceed to login.
        /// </remarks>
        public async Task<bool> IsPinSetAsync()
        {
            return File.Exists(_pinFilePath);
        }

        /// <summary>
        /// Sets the initial PIN for the system asynchronously.
        /// This method creates the PIN file and stores the hashed PIN.
        /// </summary>
        /// <param name="pin">The initial PIN to set.</param>
        /// <exception cref="ArgumentException">Thrown when the PIN is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the PIN cannot be set.</exception>
        /// <remarks>
        /// The PIN is validated for length (minimum 4 characters) before being set.
        /// The PIN is hashed using SHA-256 before storage for security.
        /// This method should only be called when no PIN exists in the system.
        /// </remarks>
        public async Task SetInitialPinAsync(string pin)
        {
            if (string.IsNullOrEmpty(pin) || pin.Length < 4)
                throw new ArgumentException("PIN must be at least 4 characters long.");

            try
            {
                string hash = HashPin(pin);
                await EnsureDirectoryExists();
                await File.WriteAllTextAsync(_pinFilePath, hash);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to set initial PIN.", ex);
            }
        }

        /// <summary>
        /// Creates a SHA-256 hash of the provided PIN for secure storage.
        /// This method ensures PINs are never stored in plain text.
        /// </summary>
        /// <param name="pin">The PIN to hash.</param>
        /// <returns>The Base64-encoded SHA-256 hash of the PIN.</returns>
        /// <remarks>
        /// Uses SHA-256 cryptographic hash function for security.
        /// The hash is returned as a Base64 string for easy storage and comparison.
        /// This method is private to ensure PINs are always hashed before storage.
        /// </remarks>
        private string HashPin(string pin)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Ensures the directory for PIN storage exists asynchronously.
        /// This method creates the necessary directory structure if it doesn't exist.
        /// </summary>
        /// <remarks>
        /// Creates the "MachineManagementSystem" directory in the user's ApplicationData folder
        /// if it doesn't already exist. This ensures the PIN file can be created successfully.
        /// </remarks>
        private async Task EnsureDirectoryExists()
        {
            string directory = Path.GetDirectoryName(_pinFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
} 