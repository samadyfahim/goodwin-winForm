using System;
using System.Threading.Tasks;
using goodwin_winForm.Services;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication and PIN-related business logic.
    /// Provides a clean interface between the UI layer and PIN service for secure authentication.
    /// </summary>
    public class AuthController : IAuthController
    {
        private readonly IPinService _pinService;
        private const int MIN_PIN_LENGTH = 4;
        private const int MAX_PIN_LENGTH = 10;

        /// <summary>
        /// Initializes a new instance of the AuthController with the specified PIN service.
        /// </summary>
        /// <param name="pinService">The PIN service for secure PIN operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when pinService is null.</exception>
        public AuthController(IPinService pinService)
        {
            _pinService = pinService ?? throw new ArgumentNullException(nameof(pinService));
        }

        /// <summary>
        /// Validates a PIN against the stored hash asynchronously.
        /// This method is used by the LoginForm to authenticate users.
        /// </summary>
        /// <param name="pin">The PIN to validate.</param>
        /// <returns>True if the PIN is valid; otherwise, false.</returns>
        /// <remarks>
        /// The PIN is validated against the SHA-256 hash stored in the PIN file.
        /// Empty or null PINs are automatically rejected.
        /// </remarks>
        public async Task<bool> ValidatePinAsync(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                return false;

            return await _pinService.ValidatePinAsync(pin);
        }

        /// <summary>
        /// Changes the current PIN to a new PIN asynchronously.
        /// This method is used by the ChangePinForm to allow users to update their PIN.
        /// </summary>
        /// <param name="currentPin">The current PIN for verification.</param>
        /// <param name="newPin">The new PIN to set.</param>
        /// <returns>True if the PIN was successfully changed; otherwise, false.</returns>
        /// <remarks>
        /// The method performs the following validations:
        /// - Both PINs must not be null or empty
        /// - New PIN must meet format requirements
        /// - Current PIN must be valid
        /// </remarks>
        public async Task<bool> ChangePinAsync(string currentPin, string newPin)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(currentPin) || string.IsNullOrWhiteSpace(newPin))
                return false;

            // Validate PIN format
            if (!await IsValidPinFormatAsync(newPin))
                return false;

            // Verify current PIN
            if (!await ValidatePinAsync(currentPin))
                return false;

            // Change PIN
            return await _pinService.ChangePinAsync(currentPin, newPin);
        }

        /// <summary>
        /// Checks if a PIN has been set in the system asynchronously.
        /// This method is used to determine if the user needs to set an initial PIN.
        /// </summary>
        /// <returns>True if a PIN is set; otherwise, false.</returns>
        /// <remarks>
        /// Used during application startup to determine if the user should be prompted
        /// to set an initial PIN or proceed to login.
        /// </remarks>
        public async Task<bool> IsPinSetAsync()
        {
            return await _pinService.IsPinSetAsync();
        }

        /// <summary>
        /// Sets the initial PIN for the system asynchronously.
        /// This method is used when setting up the application for the first time.
        /// </summary>
        /// <param name="pin">The initial PIN to set.</param>
        /// <exception cref="ArgumentException">Thrown when the PIN format is invalid.</exception>
        /// <remarks>
        /// The PIN is validated for format before being set. This method should only
        /// be called when no PIN exists in the system.
        /// </remarks>
        public async Task SetInitialPinAsync(string pin)
        {
            if (!await IsValidPinFormatAsync(pin))
                throw new ArgumentException($"PIN must be between {MIN_PIN_LENGTH} and {MAX_PIN_LENGTH} characters long.");

            await _pinService.SetInitialPinAsync(pin);
        }

        /// <summary>
        /// Validates the format of a PIN according to business rules.
        /// This method ensures PINs meet the required format before being stored.
        /// </summary>
        /// <param name="pin">The PIN to validate.</param>
        /// <returns>True if the PIN format is valid; otherwise, false.</returns>
        /// <remarks>
        /// Format requirements:
        /// - PIN must not be null or empty
        /// - PIN length must be between MIN_PIN_LENGTH and MAX_PIN_LENGTH
        /// - PIN must contain only numeric digits
        /// </remarks>
        public Task<bool> IsValidPinFormatAsync(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                return Task.FromResult(false);

            if (pin.Length < MIN_PIN_LENGTH || pin.Length > MAX_PIN_LENGTH)
                return Task.FromResult(false);

            // Check if PIN contains only digits (optional - you can modify this requirement)
            return Task.FromResult(pin.All(char.IsDigit));
        }

        /// <summary>
        /// Gets the PIN requirements as a user-friendly message.
        /// This method is used by forms to display PIN requirements to users.
        /// </summary>
        /// <returns>A string describing the PIN requirements.</returns>
        /// <remarks>
        /// Returns a formatted message that can be displayed in UI elements
        /// to inform users about PIN format requirements.
        /// </remarks>
        public string GetPinRequirements()
        {
            return $"PIN must be {MIN_PIN_LENGTH}-{MAX_PIN_LENGTH} digits long.";
        }
    }
} 