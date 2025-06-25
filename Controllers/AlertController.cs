using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goodwin_winForm.Models;
using goodwin_winForm.Services;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for managing alert-related business logic and operations.
    /// Provides a clean interface between the UI layer and data access layer for alerts.
    /// </summary>
    public class AlertController : IAlertController
    {
        private readonly IAlertRepository _alertRepository;

        /// <summary>
        /// Initializes a new instance of the AlertController with the specified alert repository.
        /// </summary>
        /// <param name="alertRepository">The alert repository for data access operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when alertRepository is null.</exception>
        public AlertController(IAlertRepository alertRepository)
        {
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
                var alerts = await _alertRepository.GetAlertsByMachineIdAsync(machineId);
                return alerts.ToList();
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
                var alerts = await _alertRepository.GetActiveAlertsByMachineIdAsync(machineId);
                return alerts.ToList();
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

            if (!await ValidateAlertDataAsync(alert))
                return false;

            try
            {
                await _alertRepository.AddAlertAsync(alert);
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

            if (!await ValidateAlertDataAsync(alert))
                return false;

            try
            {
                await _alertRepository.UpdateAlertAsync(alert);
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
                return await _alertRepository.GetAlertByIdAsync(alertId);
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
                var alerts = await _alertRepository.GetAllActiveAlertsAsync();
                return alerts.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve all active alerts", ex);
            }
        }

        /// <summary>
        /// Validates alert data according to business rules before saving.
        /// </summary>
        /// <param name="alert">The alert to validate.</param>
        /// <returns>True if the alert data is valid; otherwise, false.</returns>
        public async Task<bool> ValidateAlertDataAsync(Alert alert)
        {
            if (alert == null)
                return false;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(alert.Title))
                return false;

            if (alert.MachineId <= 0)
                return false;

            // Validate dates
            if (alert.CreatedDate > DateTime.Now.AddDays(1)) // Allow future dates up to 1 day
                return false;

            if (alert.AcknowledgedDate.HasValue && alert.AcknowledgedDate < alert.CreatedDate)
                return false;

            if (alert.ResolvedDate.HasValue && alert.ResolvedDate < alert.CreatedDate)
                return false;

            return true;
        }

        /// <summary>
        /// Creates a maintenance overdue alert for a machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="dueDate">The maintenance due date.</param>
        /// <returns>True if the alert was successfully created; otherwise, false.</returns>
        public async Task<bool> CreateMaintenanceOverdueAlertAsync(int machineId, string machineName, DateTime dueDate)
        {
            var alert = new Alert
            {
                MachineId = machineId,
                Type = AlertType.MaintenanceOverdue,
                Severity = AlertSeverity.High,
                Title = $"Maintenance Overdue - {machineName}",
                Message = $"Maintenance was due on {dueDate:d}. Please schedule maintenance immediately.",
                Status = AlertStatus.Active,
                CreatedDate = DateTime.Now
            };

            return await AddAlertAsync(alert);
        }

        /// <summary>
        /// Creates a maintenance due alert for a machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="dueDate">The maintenance due date.</param>
        /// <returns>True if the alert was successfully created; otherwise, false.</returns>
        public async Task<bool> CreateMaintenanceDueAlertAsync(int machineId, string machineName, DateTime dueDate)
        {
            var alert = new Alert
            {
                MachineId = machineId,
                Type = AlertType.MaintenanceDue,
                Severity = AlertSeverity.Medium,
                Title = $"Maintenance Due - {machineName}",
                Message = $"Maintenance is due on {dueDate:d}. Please schedule maintenance soon.",
                Status = AlertStatus.Active,
                CreatedDate = DateTime.Now
            };

            return await AddAlertAsync(alert);
        }
    }
} 