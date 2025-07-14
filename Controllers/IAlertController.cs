using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller interface responsible for alert data access operations.
    /// Provides a clean interface between the UI layer and data access layer for alerts.
    /// </summary>
    public interface IAlertController
    {
        // Data access methods only
        Task<List<Alert>> GetAlertsByMachineIdAsync(int machineId);
        Task<List<Alert>> GetActiveAlertsByMachineIdAsync(int machineId);
        Task<bool> AddAlertAsync(Alert alert);
        Task<bool> UpdateAlertAsync(Alert alert);
        Task<Alert?> GetAlertByIdAsync(int alertId);
        Task<List<Alert>> GetAllActiveAlertsAsync();
    }
} 