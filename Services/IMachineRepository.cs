using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    public interface IMachineRepository
    {
        // Business logic methods only
        Task<bool> ValidateMachineDataAsync(Machine machine);
    }
} 