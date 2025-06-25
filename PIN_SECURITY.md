# PIN Code Security Feature

## Overview

The Machine Management System now includes a PIN code authentication feature that requires users to enter a valid PIN before accessing the application.

## Features

### Default PIN

- **Default PIN**: `1234`
- This PIN is used when the application is first installed or when no custom PIN has been set

### Security Features

- **Maximum Attempts**: 3 failed attempts will close the application
- **PIN Hashing**: PINs are stored as SHA-256 hashes for security
- **PIN Requirements**: Minimum 4 characters, maximum 10 characters
- **Secure Storage**: PINs are stored in the user's AppData folder

### PIN Management

- **Change PIN**: Users can change their PIN through the Settings menu
- **PIN Validation**: Current PIN must be verified before changing to a new one
- **Confirmation**: New PIN must be entered twice to confirm

## How to Use

### First Time Setup

1. Launch the application
2. Enter the default PIN: `1234`
3. Access the main application

### Changing Your PIN

1. From the main application, go to **Settings** → **Change PIN**
2. Enter your current PIN
3. Enter your new PIN (minimum 4 characters)
4. Confirm your new PIN
5. Click **Save**

### PIN Storage Location

PINs are stored securely in:

```
%APPDATA%\MachineManagementSystem\pin.dat
```

## Security Notes

### For Production Use

Consider implementing the following additional security measures:

1. **Database Storage**: Store PINs in a secure database instead of files
2. **Encryption**: Use additional encryption layers
3. **PIN Policies**: Implement complexity requirements (numbers, letters, special characters)
4. **Account Lockout**: Implement temporary lockouts after failed attempts
5. **Audit Logging**: Log PIN change attempts and successful changes
6. **Multi-factor Authentication**: Consider adding additional authentication methods

### Current Implementation

- PINs are hashed using SHA-256
- Stored in user-specific AppData folder
- Maximum 3 login attempts
- Minimum 4-character PIN requirement

## Troubleshooting

### Forgotten PIN

If you forget your PIN and cannot access the application:

1. Delete the PIN file: `%APPDATA%\MachineManagementSystem\pin.dat`
2. Restart the application
3. Use the default PIN: `1234`
4. Set a new PIN through the Settings menu

### PIN File Corruption

If the PIN file becomes corrupted:

1. Delete the PIN file: `%APPDATA%\MachineManagementSystem\pin.dat`
2. Restart the application
3. Use the default PIN: `1234`
4. Set a new PIN through the Settings menu

## Technical Details

### PIN Service Interface

```csharp
public interface IPinService
{
    Task<bool> ValidatePinAsync(string pin);
    Task<bool> ChangePinAsync(string currentPin, string newPin);
    Task<bool> IsPinSetAsync();
    Task SetInitialPinAsync(string pin);
}
```

### PIN Hashing

PINs are hashed using SHA-256 before storage:

```csharp
private string HashPin(string pin)
{
    using (var sha256 = SHA256.Create())
    {
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
        return Convert.ToBase64String(hashBytes);
    }
}
```

### Dependency Injection

The PIN service is registered as a singleton in the application:

```csharp
services.AddSingleton<IPinService, PinService>();
```
