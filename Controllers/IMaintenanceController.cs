using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller interface responsible for maintenance data access operations.
    /// Provides a clean interface between the UI layer and data access layer for maintenance records.
    /// </summary>
    public interface IMaintenanceController
    {
        // Data access methods only
        Task<List<MaintenanceRecord>> GetMaintenanceRecordsByMachineIdAsync(int machineId);
        Task<bool> AddMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);
        Task<bool> UpdateMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);
        Task<MaintenanceRecord?> GetMaintenanceRecordByIdAsync(int maintenanceId);
    }
} 