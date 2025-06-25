using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goodwin_winForm.Models;
using goodwin_winForm.Services;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for managing maintenance-related business logic and operations.
    /// Provides a clean interface between the UI layer and data access layer for maintenance records.
    /// </summary>
    public class MaintenanceController : IMaintenanceController
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        /// <summary>
        /// Initializes a new instance of the MaintenanceController with the specified maintenance repository.
        /// </summary>
        /// <param name="maintenanceRepository">The maintenance repository for data access operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceRepository is null.</exception>
        public MaintenanceController(IMaintenanceRepository maintenanceRepository)
        {
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
                var records = await _maintenanceRepository.GetMaintenanceRecordsByMachineIdAsync(machineId);
                return records.ToList();
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

            if (!await ValidateMaintenanceRecordDataAsync(maintenanceRecord))
                return false;

            try
            {
                await _maintenanceRepository.AddMaintenanceRecordAsync(maintenanceRecord);
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

            if (!await ValidateMaintenanceRecordDataAsync(maintenanceRecord))
                return false;

            try
            {
                await _maintenanceRepository.UpdateMaintenanceRecordAsync(maintenanceRecord);
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
                return await _maintenanceRepository.GetMaintenanceRecordByIdAsync(maintenanceId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve maintenance record", ex);
            }
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