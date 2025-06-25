using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository interface responsible for maintenance record data access operations.
    /// Provides a clean interface for database operations related to maintenance records.
    /// </summary>
    public interface IMaintenanceRepository
    {
        /// <summary>
        /// Retrieves all maintenance records for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get maintenance records for.</param>
        /// <returns>A collection of maintenance records for the specified machine.</returns>
        Task<IEnumerable<MaintenanceRecord>> GetMaintenanceRecordsByMachineIdAsync(int machineId);

        /// <summary>
        /// Adds a new maintenance record to the database asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to add.</param>
        /// <returns>The added maintenance record with updated ID and timestamps.</returns>
        Task<MaintenanceRecord> AddMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);

        /// <summary>
        /// Updates an existing maintenance record in the database asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to update.</param>
        /// <returns>The updated maintenance record.</returns>
        Task<MaintenanceRecord> UpdateMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);

        /// <summary>
        /// Retrieves a specific maintenance record by ID asynchronously.
        /// </summary>
        /// <param name="maintenanceId">The ID of the maintenance record to retrieve.</param>
        /// <returns>The maintenance record if found; otherwise, null.</returns>
        Task<MaintenanceRecord?> GetMaintenanceRecordByIdAsync(int maintenanceId);
    }
} 