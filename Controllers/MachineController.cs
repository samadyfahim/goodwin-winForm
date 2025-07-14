using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goodwin_winForm.Models;
using goodwin_winForm.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for machine data access operations.
    /// Provides a clean interface between the UI layer and data access layer.
    /// </summary>
    public class MachineController : IMachineController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMachineRepository _machineRepository;
        private readonly IAlertRepository? _alertRepository;

        /// <summary>
        /// Initializes a new instance of the MachineController with the specified database context and repositories.
        /// </summary>
        /// <param name="context">The database context for data access operations.</param>
        /// <param name="machineRepository">The machine repository for business logic operations.</param>
        /// <param name="alertRepository">The alert repository for creating automatic alerts (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when context or machineRepository is null.</exception>
        public MachineController(ApplicationDbContext context, IMachineRepository machineRepository, IAlertRepository? alertRepository = null)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _alertRepository = alertRepository;
        }

        /// <summary>
        /// Retrieves all machines from the database asynchronously.
        /// </summary>
        /// <returns>A list of all machines in the system.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<Machine>> GetAllMachinesAsync()
        {
            try
            {
                if (!await _context.Database.CanConnectAsync())
                {
                    throw new InvalidOperationException("Cannot connect to database");
                }
                
                var machines = await _context.Machines
                    .Include(m => m.MaintenanceRecords)
                    .Include(m => m.Alerts.Where(a => a.Status == AlertStatus.Active))
                    .OrderBy(m => m.Name)
                    .ToListAsync();
                return machines;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve machines", ex);
            }
        }

        /// <summary>
        /// Retrieves a single machine by its ID from the database asynchronously.
        /// </summary>
        /// <param name="machineId">The ID of the machine to retrieve.</param>
        /// <returns>The machine object if found; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<Machine?> GetMachineByIdAsync(int machineId)
        {
            try
            {
                var machines = await GetAllMachinesAsync();
                return machines.FirstOrDefault(m => m.MachineId == machineId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve machine", ex);
            }
        }

        /// <summary>
        /// Adds a new machine to the system asynchronously.
        /// </summary>
        /// <param name="machine">The machine object to add to the system.</param>
        /// <returns>True if the machine was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when machine is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> AddMachineAsync(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!await _machineRepository.ValidateMachineDataAsync(machine))
                return false;

            try
            {
                machine.CreatedAt = DateTime.Now;
                machine.UpdatedAt = DateTime.Now;
                
                _context.Machines.Add(machine);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add machine", ex);
            }
        }

        /// <summary>
        /// Updates an existing machine in the system asynchronously.
        /// </summary>
        /// <param name="machine">The machine object to update in the system.</param>
        /// <returns>True if the machine was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when machine is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> UpdateMachineAsync(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!await _machineRepository.ValidateMachineDataAsync(machine))
                return false;

            try
            {
                if (machine.MachineId <= 0)
                {
                    throw new ArgumentException("Machine ID must be greater than 0", nameof(machine));
                }
                
                machine.UpdatedAt = DateTime.Now;
                
                var existingMachine = await _context.Machines
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MachineId == machine.MachineId);
                
                if (existingMachine == null)
                {
                    throw new InvalidOperationException($"Machine with ID {machine.MachineId} not found");
                }
                
                var sql = @"
                    UPDATE Machines 
                    SET Name = @Name, Description = @Description, SerialNumber = @SerialNumber,
                        Model = @Model, Manufacturer = @Manufacturer, InstallationDate = @InstallationDate,
                        Status = @Status, Location = @Location, Department = @Department,
                        LastMaintenanceDate = @LastMaintenanceDate, NextMaintenanceDate = @NextMaintenanceDate,
                        MaintenanceIntervalDays = @MaintenanceIntervalDays, Notes = @Notes,
                        ImagePath = @ImagePath, UpdatedAt = @UpdatedAt
                    WHERE MachineId = @MachineId";
                
                var parameters = new[]
                {
                    new SqlParameter("@MachineId", machine.MachineId),
                    new SqlParameter("@Name", machine.Name),
                    new SqlParameter("@Description", (object)machine.Description ?? DBNull.Value),
                    new SqlParameter("@SerialNumber", machine.SerialNumber),
                    new SqlParameter("@Model", machine.Model),
                    new SqlParameter("@Manufacturer", (object)machine.Manufacturer ?? DBNull.Value),
                    new SqlParameter("@InstallationDate", machine.InstallationDate),
                    new SqlParameter("@Status", (int)machine.Status),
                    new SqlParameter("@Location", (object)machine.Location ?? DBNull.Value),
                    new SqlParameter("@Department", (object)machine.Department ?? DBNull.Value),
                    new SqlParameter("@LastMaintenanceDate", machine.LastMaintenanceDate),
                    new SqlParameter("@NextMaintenanceDate", machine.NextMaintenanceDate),
                    new SqlParameter("@MaintenanceIntervalDays", machine.MaintenanceIntervalDays),
                    new SqlParameter("@Notes", (object)machine.Notes ?? DBNull.Value),
                    new SqlParameter("@ImagePath", (object)machine.ImagePath ?? DBNull.Value),
                    new SqlParameter("@UpdatedAt", machine.UpdatedAt)
                };
                
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sql, parameters);
                
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No rows were updated for machine ID {machine.MachineId}");
                }
                
                await CheckAndCreateMaintenanceAlertsAsync(machine);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update machine", ex);
            }
        }

        /// <summary>
        /// Checks for maintenance overdue conditions and creates alerts automatically.
        /// </summary>
        /// <param name="machine">The machine to check for maintenance alerts.</param>
        private async Task CheckAndCreateMaintenanceAlertsAsync(Machine machine)
        {
            if (_alertRepository == null)
                return;

            try
            {
                // Check if maintenance is overdue (NextMaintenanceDate < Today)
                if (machine.NextMaintenanceDate != DateTime.MinValue && machine.NextMaintenanceDate < DateTime.Today)
                {
                    await _alertRepository.CreateMaintenanceOverdueAlertAsync(
                        machine.MachineId, 
                        machine.Name, 
                        machine.NextMaintenanceDate);
                }
                // Check if maintenance is due soon (within 7 days)
                else if (machine.NextMaintenanceDate != DateTime.MinValue && 
                         machine.NextMaintenanceDate <= DateTime.Today.AddDays(7) &&
                         machine.NextMaintenanceDate >= DateTime.Today)
                {
                    await _alertRepository.CreateMaintenanceDueAlertAsync(
                        machine.MachineId, 
                        machine.Name, 
                        machine.NextMaintenanceDate);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the machine update
                System.Diagnostics.Debug.WriteLine($"Error creating maintenance alerts: {ex.Message}");
            }
        }
    }
} 