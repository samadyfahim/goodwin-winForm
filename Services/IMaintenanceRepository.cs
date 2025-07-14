using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository interface responsible for maintenance record business logic operations.
    /// Provides business logic for maintenance records.
    /// </summary>
    public interface IMaintenanceRepository
    {
        // Business logic methods only
        Task<bool> ValidateMaintenanceRecordDataAsync(MaintenanceRecord maintenanceRecord);
    }
} 