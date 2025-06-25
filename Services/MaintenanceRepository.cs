using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for maintenance record data access operations using Entity Framework Core.
    /// Provides a clean interface for database operations related to maintenance records.
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
        /// Retrieves all maintenance records for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get maintenance records for.</param>
        /// <returns>A collection of maintenance records for the specified machine.</returns>
        public async Task<IEnumerable<MaintenanceRecord>> GetMaintenanceRecordsByMachineIdAsync(int machineId)
        {
            return await _context.MaintenanceRecords
                .Where(m => m.MachineId == machineId)
                .OrderByDescending(m => m.MaintenanceDate)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new maintenance record to the database asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to add.</param>
        /// <returns>The added maintenance record with updated ID and timestamps.</returns>
        public async Task<MaintenanceRecord> AddMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord)
        {
            maintenanceRecord.CreatedAt = DateTime.Now;
            maintenanceRecord.UpdatedAt = DateTime.Now;
            
            _context.MaintenanceRecords.Add(maintenanceRecord);
            await _context.SaveChangesAsync();
            return maintenanceRecord;
        }

        /// <summary>
        /// Updates an existing maintenance record in the database asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to update.</param>
        /// <returns>The updated maintenance record.</returns>
        public async Task<MaintenanceRecord> UpdateMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord)
        {
            maintenanceRecord.UpdatedAt = DateTime.Now;
            
            _context.MaintenanceRecords.Update(maintenanceRecord);
            await _context.SaveChangesAsync();
            return maintenanceRecord;
        }

        /// <summary>
        /// Retrieves a specific maintenance record by ID asynchronously.
        /// </summary>
        /// <param name="maintenanceId">The ID of the maintenance record to retrieve.</param>
        /// <returns>The maintenance record if found; otherwise, null.</returns>
        public async Task<MaintenanceRecord?> GetMaintenanceRecordByIdAsync(int maintenanceId)
        {
            return await _context.MaintenanceRecords
                .FirstOrDefaultAsync(m => m.MaintenanceId == maintenanceId);
        }
    }
} 