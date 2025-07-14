using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for maintenance record business logic operations using Entity Framework Core.
    /// Provides business logic for maintenance records.
    /// </summary>
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the MaintenanceRepository with the specified database context.
        /// </summary>
        /// <param name="context">The Entity Framework database context for data access.</param>
        public MaintenanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Validates maintenance record data according to business rules before saving.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to validate.</param>
        /// <returns>True if the maintenance record data is valid; otherwise, false.</returns>
        public async Task<bool> ValidateMaintenanceRecordDataAsync(MaintenanceRecord maintenanceRecord)
        {
            if (maintenanceRecord == null)
                return false;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(maintenanceRecord.Title))
                return false;

            if (string.IsNullOrWhiteSpace(maintenanceRecord.PerformedBy))
                return false;

            if (maintenanceRecord.MachineId <= 0)
                return false;

            // Validate dates
            if (maintenanceRecord.MaintenanceDate > DateTime.Today.AddDays(30)) // Allow scheduling up to 30 days in advance
                return false;

            if (maintenanceRecord.CompletedDate.HasValue && maintenanceRecord.CompletedDate < maintenanceRecord.MaintenanceDate)
                return false;

            // Validate cost (should be non-negative)
            if (maintenanceRecord.Cost < 0)
                return false;

            return true;
        }
    }
} 