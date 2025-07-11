using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;
using Microsoft.Data.SqlClient;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for machine data access operations using Entity Framework Core.
    /// Provides a clean interface for database operations related to machines and related entities.
    /// </summary>
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the MachineRepository with the specified database context.
        /// </summary>
        /// <param name="context">The Entity Framework database context for data access.</param>
        public MachineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all machines from the database asynchronously with related data.
        /// This method includes maintenance records and active alerts for each machine.
        /// </summary>
        /// <returns>A collection of all machines ordered by name.</returns>
        /// <remarks>
        /// Includes related data:
        /// - Maintenance records for each machine
        /// - Active alerts for each machine
        /// Results are ordered alphabetically by machine name.
        /// </remarks>
        public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
        {
            try
            {
                // Test database connection first
                if (!await _context.Database.CanConnectAsync())
                {
                    throw new InvalidOperationException("Cannot connect to database");
                }
                
                return await _context.Machines
                    .Include(m => m.MaintenanceRecords)
                    .Include(m => m.Alerts.Where(a => a.Status == AlertStatus.Active))
                    .OrderBy(m => m.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllMachinesAsync failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Adds a new machine to the database asynchronously.
        /// This method sets creation and update timestamps automatically.
        /// </summary>
        /// <param name="machine">The machine object to add to the database.</param>
        /// <returns>The added machine with updated ID and timestamps.</returns>
        /// <remarks>
        /// Automatically sets:
        /// - CreatedAt timestamp to current date/time
        /// - UpdatedAt timestamp to current date/time
        /// The machine ID will be generated by the database.
        /// </remarks>
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            machine.CreatedAt = DateTime.Now;
            machine.UpdatedAt = DateTime.Now;
            
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();
            return machine;
        }

        /// <summary>
        /// Updates an existing machine in the database asynchronously.
        /// This method updates the machine data while preserving creation timestamp.
        /// </summary>
        /// <param name="machine">The machine object to update in the database.</param>
        /// <returns>The updated machine with updated timestamp.</returns>
        /// <remarks>
        /// Automatically sets:
        /// - UpdatedAt timestamp to current date/time
        /// Preserves the original CreatedAt timestamp.
        /// </remarks>
        public async Task<Machine> UpdateMachineAsync(Machine machine)
        {
            System.Diagnostics.Debug.WriteLine($"Repository: Starting update for machine ID {machine.MachineId}");
            
            if (machine.MachineId <= 0)
            {
                throw new ArgumentException("Machine ID must be greater than 0", nameof(machine));
            }
            
            machine.UpdatedAt = DateTime.Now;
            
            try
            {
                // Use a more explicit approach to avoid tracking issues
                var existingMachine = await _context.Machines
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MachineId == machine.MachineId);
                
                if (existingMachine == null)
                {
                    throw new InvalidOperationException($"Machine with ID {machine.MachineId} not found");
                }
                
                System.Diagnostics.Debug.WriteLine($"Repository: Found existing machine, updating values");
                
                // Update the machine using raw SQL to avoid any tracking issues
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
                    new Microsoft.Data.SqlClient.SqlParameter("@MachineId", machine.MachineId),
                    new Microsoft.Data.SqlClient.SqlParameter("@Name", machine.Name),
                    new Microsoft.Data.SqlClient.SqlParameter("@Description", (object)machine.Description ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@SerialNumber", machine.SerialNumber),
                    new Microsoft.Data.SqlClient.SqlParameter("@Model", machine.Model),
                    new Microsoft.Data.SqlClient.SqlParameter("@Manufacturer", (object)machine.Manufacturer ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@InstallationDate", machine.InstallationDate),
                    new Microsoft.Data.SqlClient.SqlParameter("@Status", (int)machine.Status),
                    new Microsoft.Data.SqlClient.SqlParameter("@Location", (object)machine.Location ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@Department", (object)machine.Department ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@LastMaintenanceDate", machine.LastMaintenanceDate),
                    new Microsoft.Data.SqlClient.SqlParameter("@NextMaintenanceDate", machine.NextMaintenanceDate),
                    new Microsoft.Data.SqlClient.SqlParameter("@MaintenanceIntervalDays", machine.MaintenanceIntervalDays),
                    new Microsoft.Data.SqlClient.SqlParameter("@Notes", (object)machine.Notes ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@ImagePath", (object)machine.ImagePath ?? DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@UpdatedAt", machine.UpdatedAt)
                };
                
                System.Diagnostics.Debug.WriteLine($"Repository: Executing SQL update...");
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sql, parameters);
                
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No rows were updated for machine ID {machine.MachineId}");
                }
                
                System.Diagnostics.Debug.WriteLine($"Repository: SQL update completed successfully, {rowsAffected} rows affected");
                
                // Return the updated machine
                return machine;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Repository: Update failed with error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Repository: Error type: {ex.GetType().Name}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Repository: Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
} 