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
    /// Controller responsible for maintenance data access operations.
    /// Provides a clean interface between the UI layer and data access layer for maintenance records.
    /// </summary>
    public class MaintenanceController : IMaintenanceController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMaintenanceRepository _maintenanceRepository;

        /// <summary>
        /// Initializes a new instance of the MaintenanceController with the specified database context and maintenance repository.
        /// </summary>
        /// <param name="context">The database context for data access operations.</param>
        /// <param name="maintenanceRepository">The maintenance repository for business logic operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when context or maintenanceRepository is null.</exception>
        public MaintenanceController(ApplicationDbContext context, IMaintenanceRepository maintenanceRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _maintenanceRepository = maintenanceRepository ?? throw new ArgumentNullException(nameof(maintenanceRepository));
        }

        /// <summary>
        /// Retrieves all maintenance records for a specific machine asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to get maintenance records for.</param>
        /// <returns>A list of maintenance records for the specified machine.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<MaintenanceRecord>> GetMaintenanceRecordsByMachineIdAsync(int machineId)
        {
            try
            {
                var records = await _context.MaintenanceRecords
                    .Where(m => m.MachineId == machineId)
                    .OrderByDescending(m => m.MaintenanceDate)
                    .ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve maintenance records", ex);
            }
        }

        /// <summary>
        /// Adds a new maintenance record to the system asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to add.</param>
        /// <returns>True if the maintenance record was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceRecord is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> AddMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord)
        {
            if (maintenanceRecord == null)
                throw new ArgumentNullException(nameof(maintenanceRecord));

            if (!await _maintenanceRepository.ValidateMaintenanceRecordDataAsync(maintenanceRecord))
                return false;

            try
            {
                maintenanceRecord.CreatedAt = DateTime.Now;
                maintenanceRecord.UpdatedAt = DateTime.Now;
                
                _context.MaintenanceRecords.Add(maintenanceRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add maintenance record", ex);
            }
        }

        /// <summary>
        /// Updates an existing maintenance record in the system asynchronously.
        /// </summary>
        /// <param name="maintenanceRecord">The maintenance record to update.</param>
        /// <returns>True if the maintenance record was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceRecord is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> UpdateMaintenanceRecordAsync(MaintenanceRecord maintenanceRecord)
        {
            if (maintenanceRecord == null)
                throw new ArgumentNullException(nameof(maintenanceRecord));

            if (!await _maintenanceRepository.ValidateMaintenanceRecordDataAsync(maintenanceRecord))
                return false;

            try
            {
                maintenanceRecord.UpdatedAt = DateTime.Now;
                
                _context.MaintenanceRecords.Update(maintenanceRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update maintenance record", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific maintenance record by ID asynchronously.
        /// </summary>
        /// <param name="maintenanceId">The ID of the maintenance record to retrieve.</param>
        /// <returns>The maintenance record if found; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<MaintenanceRecord?> GetMaintenanceRecordByIdAsync(int maintenanceId)
        {
            try
            {
                return await _context.MaintenanceRecords
                    .FirstOrDefaultAsync(m => m.MaintenanceId == maintenanceId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve maintenance record", ex);
            }
        }
    }
} 