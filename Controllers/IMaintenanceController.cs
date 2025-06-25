using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller interface responsible for managing maintenance-related business logic and operations.
    /// Provides a clean interface between the UI layer and data access layer for maintenance records.
    /// </summary>
    public interface IMaintenanceController
    {
        /// <summary>
        /// Retrieves all maintenance records for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get maintenance records for.</param>
        /// <returns>A list of maintenance records for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<List<MaintenanceRecord>> GetMaintenanceRecordsByMachineIdAsync(int machineId);

        /// <summary>
        /// Adds a new maintenance record to the system asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to add.</param>
        /// <returns>True if the maintenance record was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceRecord is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<bool> AddMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);

        /// <summary>
        /// Updates an existing maintenance record in the system asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to update.</param>
        /// <returns>True if the maintenance record was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceRecord is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<bool> UpdateMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord);

        /// <summary>
        /// Retrieves a specific maintenance record by ID asynchronously.
        /// </summary>
        /// <param name="maintenanceId">The ID of the maintenance record to retrieve.</param>
        /// <returns>The maintenance record if found; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<MaintenanceRecord?> GetMaintenanceRecordByIdAsync(int maintenanceId);

        /// <summary>
        /// Validates maintenance record data according to business rules before saving.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to validate.</param>
        /// <returns>True if the maintenance record data is valid; otherwise, false.</returns>
        Task<bool> ValidateMaintenanceRecordDataAsync(MaintenanceRecord maintenanceRecord);
    }
} 