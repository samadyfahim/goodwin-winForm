using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goodwin_winForm.Models;
using goodwin_winForm.Services;

namespace goodwin_winForm.Controllers
{
    /// <summary>
    /// Controller responsible for managing machine-related business logic and operations.
    /// Provides a clean interface between the UI layer and data access layer.
    /// </summary>
    public class MachineController : IMachineController
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IAlertController? _alertController;

        /// <summary>
        /// Initializes a new instance of the MachineController with the specified machine repository.
        /// </summary>
        /// <param name="machineRepository">The machine repository for data access operations.</param>
        /// <param name="alertController">The alert controller for creating automatic alerts (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when machineRepository is null.</exception>
        public MachineController(IMachineRepository machineRepository, IAlertController? alertController = null)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _alertController = alertController;
        }

        /// <summary>
        /// Retrieves all machines from the database asynchronously.
        /// This method is used by the main machine selection form to display the list of available machines.
        /// </summary>
        /// <returns>A list of all machines in the system.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<List<Machine>> GetAllMachinesAsync()
        {
            try
            {
                var machines = await _machineRepository.GetAllMachinesAsync();
                return machines.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve machines", ex);
            }
        }

        /// <summary>
        /// Adds a new machine to the system asynchronously.
        /// This method validates the machine data before saving it to the database.
        /// Used by the AddMachineForm to create new machine entries.
        /// </summary>
        /// <param name="machine">The machine object to add to the system.</param>
        /// <returns>True if the machine was successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when machine is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> AddMachineAsync(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            if (!await ValidateMachineDataAsync(machine))
                return false;

            try
            {
                await _machineRepository.AddMachineAsync(machine);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add machine", ex);
            }
        }

        /// <summary>
        /// Updates an existing machine in the system asynchronously.
        /// This method validates the machine data before updating it in the database.
        /// Used by the EditMachineForm to update existing machine entries.
        /// </summary>
        /// <param name="machine">The machine object to update in the system.</param>
        /// <returns>True if the machine was successfully updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when machine is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the database operation fails.</exception>
        public async Task<bool> UpdateMachineAsync(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            // Add debugging information
            System.Diagnostics.Debug.WriteLine($"Updating machine: ID={machine.MachineId}, Name={machine.Name}, Serial={machine.SerialNumber}");
            System.Diagnostics.Debug.WriteLine($"Machine details: Model={machine.Model}, Manufacturer={machine.Manufacturer}");
            System.Diagnostics.Debug.WriteLine($"Dates: Installation={machine.InstallationDate}, LastMaintenance={machine.LastMaintenanceDate}, NextMaintenance={machine.NextMaintenanceDate}");

            if (!await ValidateMachineDataAsync(machine))
            {
                System.Diagnostics.Debug.WriteLine("Machine validation failed");
                return false;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine("Calling repository UpdateMachineAsync...");
                await _machineRepository.UpdateMachineAsync(machine);
                System.Diagnostics.Debug.WriteLine("Machine updated successfully");
                await CheckAndCreateMaintenanceAlertsAsync(machine);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating machine: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new InvalidOperationException("Failed to update machine", ex);
            }
        }

        /// <summary>
        /// Validates machine data according to business rules before saving.
        /// This method ensures that all required fields are present and valid,
        /// and checks for duplicate serial numbers to maintain data integrity.
        /// </summary>
        /// <param name="machine">The machine object to validate.</param>
        /// <returns>True if the machine data is valid; otherwise, false.</returns>
        /// <remarks>
        /// Validation rules:
        /// - Name, SerialNumber, Model, and Manufacturer are required
        /// - Installation date cannot be in the future
        /// - Next maintenance date must be after last maintenance date
        /// - Serial number must be unique across all machines
        /// </remarks>
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
                var existingMachines = await _machineRepository.GetAllMachinesAsync();
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
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error checking for duplicate serial numbers: {ex.Message}");
                // If we can't check for duplicates, assume it's valid to avoid blocking updates
                return true;
            }

            System.Diagnostics.Debug.WriteLine("Machine validation passed successfully");
            return true;
        }

        /// <summary>
        /// Checks for maintenance overdue conditions and creates alerts automatically.
        /// </summary>
        /// <param name="machine">The machine to check for maintenance alerts.</param>
        private async Task CheckAndCreateMaintenanceAlertsAsync(Machine machine)
        {
            if (_alertController == null)
                return;

            try
            {
                // Check if maintenance is overdue (NextMaintenanceDate < Today)
                if (machine.NextMaintenanceDate != DateTime.MinValue && machine.NextMaintenanceDate < DateTime.Today)
                {
                    await _alertController.CreateMaintenanceOverdueAlertAsync(
                        machine.MachineId, 
                        machine.Name, 
                        machine.NextMaintenanceDate);
                }
                // Check if maintenance is due soon (within 7 days)
                else if (machine.NextMaintenanceDate != DateTime.MinValue && 
                         machine.NextMaintenanceDate <= DateTime.Today.AddDays(7) &&
                         machine.NextMaintenanceDate >= DateTime.Today)
                {
                    await _alertController.CreateMaintenanceDueAlertAsync(
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