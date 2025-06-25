using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for alert data access operations using Entity Framework Core.
    /// Provides a clean interface for database operations related to alerts.
    /// </summary>
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AlertRepository with the specified database context.
        /// </summary>
        /// <param name="context">The Entity Framework database context for data access.</param>
        public AlertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get alerts for.</param>
        /// <returns>A collection of alerts for the specified machine.</returns>
        public async Task<IEnumerable<Alert>> GetAlertsByMachineIdAsync(int machineId)
        {
            return await _context.Alerts
                .Where(a => a.MachineId == machineId)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all active alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get active alerts for.</param>
        /// <returns>A collection of active alerts for the specified machine.</returns>
        public async Task<IEnumerable<Alert>> GetActiveAlertsByMachineIdAsync(int machineId)
        {
            return await _context.Alerts
                .Where(a => a.MachineId == machineId && a.Status == AlertStatus.Active)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new alert to the database asynchronously.
        /// </summary>
        /// <param name="alert">The alert to add.</param>
        /// <returns>The added alert with updated ID and timestamps.</returns>
        public async Task<Alert> AddAlertAsync(Alert alert)
        {
            alert.CreatedDate = DateTime.Now;
            
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
            return alert;
        }

        /// <summary>
        /// Updates an existing alert in the database asynchronously.
        /// </summary>
        /// <param name="alert">The alert to update.</param>
        /// <returns>The updated alert.</returns>
        public async Task<Alert> UpdateAlertAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
            return alert;
        }

        /// <summary>
        /// Retrieves a specific alert by ID asynchronously.
        /// </summary>
        /// <param name="alertId">The ID of the alert to retrieve.</param>
        /// <returns>The alert if found; otherwise, null.</returns>
        public async Task<Alert?> GetAlertByIdAsync(int alertId)
        {
            return await _context.Alerts
                .FirstOrDefaultAsync(a => a.AlertId == alertId);
        }

        /// <summary>
        /// Retrieves all active alerts across all machines asynchronously.
        /// </summary>
        /// <returns>A collection of all active alerts.</returns>
        public async Task<IEnumerable<Alert>> GetAllActiveAlertsAsync()
        {
            return await _context.Alerts
                .Where(a => a.Status == AlertStatus.Active)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }
    }
} 