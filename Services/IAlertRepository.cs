using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository interface responsible for alert data access operations.
    /// Provides a clean interface for database operations related to alerts.
    /// </summary>
    public interface IAlertRepository
    {
        /// <summary>
        /// Retrieves all alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get alerts for.</param>
        /// <returns>A collection of alerts for the specified machine.</returns>
        Task<IEnumerable<Alert>> GetAlertsByMachineIdAsync(int machineId);

        /// <summary>
        /// Retrieves all active alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get active alerts for.</param>
        /// <returns>A collection of active alerts for the specified machine.</returns>
        Task<IEnumerable<Alert>> GetActiveAlertsByMachineIdAsync(int machineId);

        /// <summary>
        /// Adds a new alert to the database asynchronously.
        /// </summary>
        /// <param name="alert">The alert to add.</param>
        /// <returns>The added alert with updated ID and timestamps.</returns>
        Task<Alert> AddAlertAsync(Alert alert);

        /// <summary>
        /// Updates an existing alert in the database asynchronously.
        /// </summary>
        /// <param name="alert">The alert to update.</param>
        /// <returns>The updated alert.</returns>
        Task<Alert> UpdateAlertAsync(Alert alert);

        /// <summary>
        /// Retrieves a specific alert by ID asynchronously.
        /// </summary>
        /// <param name="alertId">The ID of the alert to retrieve.</param>
        /// <returns>The alert if found; otherwise, null.</returns>
        Task<Alert?> GetAlertByIdAsync(int alertId);

        /// <summary>
        /// Retrieves all active alerts across all machines asynchronously.
        /// </summary>
        /// <returns>A collection of all active alerts.</returns>
        Task<IEnumerable<Alert>> GetAllActiveAlertsAsync();
    }
} 