using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goodwin_winForm.Models;
using goodwin_winForm.Services;
using Microsoft.EntityFrameworkCore;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for alert data access operations.
    /// Provides a clean interface between the UI layer and data access layer for alerts.
    /// </summary>
    public class AlertController : IAlertController
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlertRepository _alertRepository;

        /// <summary>
        /// Initializes a new instance of the AlertController with the specified database context and alert repository.
        /// </summary>
        /// <param name="context">The database context for data access operations.</param>
        /// <param name="alertRepository">The alert repository for business logic operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when context or alertRepository is null.</exception>
        public AlertController(ApplicationDbContext context, IAlertRepository alertRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _alertRepository = alertRepository ?? throw new ArgumentNullException(nameof(alertRepository));
        }

        /// <summary>
        /// Retrieves all alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get alerts for.</param>
        /// <returns>A list of alerts for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<Alert>> GetAlertsByMachineIdAsync(int machineId)
        {
            try
            {
                var alerts = await _context.Alerts
                    .Where(a => a.MachineId == machineId)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToListAsync();
                return alerts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve alerts", ex);
            }
        }

        /// <summary>
        /// Retrieves all active alerts for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get active alerts for.</param>
        /// <returns>A list of active alerts for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<Alert>> GetActiveAlertsByMachineIdAsync(int machineId)
        {
            try
            {
                var alerts = await _context.Alerts
                    .Where(a => a.MachineId == machineId && a.Status == AlertStatus.Active)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToListAsync();
                return alerts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve active alerts", ex);
            }
        }

        /// <summary>
        /// Adds a new alert to the system asynchronously.
        /// </summary>
        /// <param name="alert">The alert to add.</param>
        /// <returns>True if the alert was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when alert is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> AddAlertAsync(Alert alert)
        {
            if (alert == null)
                throw new ArgumentNullException(nameof(alert));

            if (!await _alertRepository.ValidateAlertDataAsync(alert))
                return false;

            try
            {
                alert.CreatedDate = DateTime.Now;
                _context.Alerts.Add(alert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add alert", ex);
            }
        }

        /// <summary>
        /// Updates an existing alert in the system asynchronously.
        /// </summary>
        /// <param name="alert">The alert to update.</param>
        /// <returns>True if the alert was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when alert is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> UpdateAlertAsync(Alert alert)
        {
            if (alert == null)
                throw new ArgumentNullException(nameof(alert));

            if (!await _alertRepository.ValidateAlertDataAsync(alert))
                return false;

            try
            {
                _context.Alerts.Update(alert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update alert", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific alert by ID asynchronously.
        /// </summary>
        /// <param name="alertId">The ID of the alert to retrieve.</param>
        /// <returns>The alert if found; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<Alert?> GetAlertByIdAsync(int alertId)
        {
            try
            {
                return await _context.Alerts
                    .FirstOrDefaultAsync(a => a.AlertId == alertId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve alert", ex);
            }
        }

        /// <summary>
        /// Retrieves all active alerts across all machines asynchronously.
        /// </summary>
        /// <returns>A list of all active alerts.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<Alert>> GetAllActiveAlertsAsync()
        {
            try
            {
                var alerts = await _context.Alerts
                    .Where(a => a.Status == AlertStatus.Active)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToListAsync();
                return alerts;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve all active alerts", ex);
            }
        }
    }
} 