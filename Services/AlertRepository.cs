using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for alert business logic operations using Entity Framework Core.
    /// Provides business logic for alerts.
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

            return await AddAlertWithValidationAsync(alert);
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

            return await AddAlertWithValidationAsync(alert);
        }

        /// <summary>
        /// Adds an alert with validation.
        /// </summary>
        /// <param name="alert">The alert to add.</param>
        /// <returns>True if the alert was successfully added; otherwise, false.</returns>
        private async Task<bool> AddAlertWithValidationAsync(Alert alert)
        {
            if (!await ValidateAlertDataAsync(alert))
                return false;

            try
            {
                alert.CreatedDate = DateTime.Now;
                _context.Alerts.Add(alert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 