using System.Threading.Tasks;

namespace goodwin_winForm.Controllers
{
    public interface IAuthController
    {
        Task<bool> ValidatePinAsync(string pin);
        Task<bool> ChangePinAsync(string currentPin, string newPin);
        Task<bool> IsPinSetAsync();
        Task SetInitialPinAsync(string pin);
        Task<bool> IsValidPinFormatAsync(string pin);
        string GetPinRequirements();
    }
} 