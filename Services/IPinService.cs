using System.Threading.Tasks;

namespace goodwin_winForm.Services
{
    public interface IPinService
    {
        Task<bool> ValidatePinAsync(string pin);
        Task<bool> ChangePinAsync(string currentPin, string newPin);
        Task<bool> IsPinSetAsync();
        Task SetInitialPinAsync(string pin);
    }
} 