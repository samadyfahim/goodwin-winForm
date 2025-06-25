using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller interface responsible for managing alert-related business logic and operations.
    /// Provides a clean interface between the UI layer and data access layer for alerts.
    /// </summary>
    public interface IAlertController
    {
        /// <summary>
        /// Retrieves all alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get alerts for.</param>
        /// <returns>A list of alerts for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<List<Alert>> GetAlertsByMachineIdAsync(int machineId);

        /// <summary>
        /// Retrieves all active alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get active alerts for.</param>
        /// <returns>A list of active alerts for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<List<Alert>> GetActiveAlertsByMachineIdAsync(int machineId);

        /// <summary>
        /// Adds a new alert to the system asynchronously.
        /// </summary>
        /// <param name="alert">The alert to add.</param>
        /// <returns>True if the alert was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when alert is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<bool> AddAlertAsync(Alert alert);

        /// <summary>
        /// Updates an existing alert in the system asynchronously.
        /// </summary>
        /// <param name="alert">The alert to update.</param>
        /// <returns>True if the alert was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when alert is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<bool> UpdateAlertAsync(Alert alert);

        /// <summary>
        /// Retrieves a specific alert by ID asynchronously.
        /// </summary>
        /// <param name="alertId">The ID of the alert to retrieve.</param>
        /// <returns>The alert if found; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<Alert?> GetAlertByIdAsync(int alertId);

        /// <summary>
        /// Retrieves all active alerts across all machines asynchronously.
        /// </summary>
        /// <returns>A list of all active alerts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        Task<List<Alert>> GetAllActiveAlertsAsync();

        /// <summary>
        /// Validates alert data according to business rules before saving.
        /// </summary>
        /// <param name="alert">The alert to validate.</param>
        /// <returns>True if the alert data is valid; otherwise, false.</returns>
        Task<bool> ValidateAlertDataAsync(Alert alert);

        /// <summary>
        /// Creates a maintenance overdue alert for a machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="dueDate">The maintenance due date.</param>
        /// <returns>True if the alert was successfully created; otherwise, false.</returns>
        Task<bool> CreateMaintenanceOverdueAlertAsync(int machineId, string machineName, DateTime dueDate);

        /// <summary>
        /// Creates a maintenance due alert for a machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="dueDate">The maintenance due date.</param>
        /// <returns>True if the alert was successfully created; otherwise, false.</returns>
        Task<bool> CreateMaintenanceDueAlertAsync(int machineId, string machineName, DateTime dueDate);
    }
} 