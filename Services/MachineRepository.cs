using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;
using Microsoft.Data.SqlClient;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Repository responsible for machine business logic operations using Entity Framework Core.
    /// Provides business logic for machines.
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
        /// Validates machine data according to business rules before saving.
        /// </summary>
        /// <param name="machine">The machine object to validate.</param>
        /// <returns>True if the machine data is valid; otherwise, false.</returns>
        public async Task<bool> ValidateMachineDataAsync(Machine machine)
        {
            if (machine == null)
            {
                System.Diagnostics.Debug.WriteLine("Validation failed: Machine is null");
                return false;
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(machine.Name))
            {
                System.Diagnostics.Debug.WriteLine("Validation failed: Name is empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(machine.SerialNumber))
            {
                System.Diagnostics.Debug.WriteLine("Validation failed: SerialNumber is empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(machine.Model))
            {
                System.Diagnostics.Debug.WriteLine("Validation failed: Model is empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(machine.Manufacturer))
            {
                System.Diagnostics.Debug.WriteLine("Validation failed: Manufacturer is empty");
                return false;
            }

            // Validate dates
            if (machine.InstallationDate > DateTime.Today)
            {
                System.Diagnostics.Debug.WriteLine($"Validation failed: InstallationDate {machine.InstallationDate} is in the future");
                return false;
            }

            // Only validate maintenance dates if they are not default values
            if (machine.LastMaintenanceDate != DateTime.MinValue && machine.NextMaintenanceDate != DateTime.MinValue)
            {
                if (machine.NextMaintenanceDate < machine.LastMaintenanceDate)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation failed: NextMaintenanceDate {machine.NextMaintenanceDate} is before LastMaintenanceDate {machine.LastMaintenanceDate}");
                    return false;
                }
            }

            // Check for duplicate serial number (excluding the current machine being updated)
            try
            {
                var existingMachines = await _context.Machines
                    .Include(m => m.MaintenanceRecords)
                    .Include(m => m.Alerts.Where(a => a.Status == AlertStatus.Active))
                    .OrderBy(m => m.Name)
                    .ToListAsync();
                    
                var duplicateSerial = existingMachines.Any(m => 
                    m.SerialNumber.Equals(machine.SerialNumber, StringComparison.OrdinalIgnoreCase) && 
                    m.MachineId != machine.MachineId);
                
                if (duplicateSerial)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation failed: Duplicate serial number found: {machine.SerialNumber}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking for duplicate serial numbers: {ex.Message}");
                return true;
            }

            System.Diagnostics.Debug.WriteLine("Machine validation passed successfully");
            return true;
        }
    }
} 