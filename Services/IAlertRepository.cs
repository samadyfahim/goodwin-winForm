using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository interface responsible for alert business logic operations.
    /// Provides business logic for alerts.
    /// </summary>
    public interface IAlertRepository
    {
        // Business logic methods only
        Task<bool> ValidateAlertDataAsync(Alert alert);
        Task<bool> CreateMaintenanceOverdueAlertAsync(int machineId, string machineName, DateTime dueDate);
        Task<bool> CreateMaintenanceDueAlertAsync(int machineId, string machineName, DateTime dueDate);
    }
} 