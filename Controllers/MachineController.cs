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

        /// <summary>
        /// Initializes a new instance of the MachineController with the specified machine repository.
        /// </summary>
        /// <param name="machineRepository">The machine repository for data access operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when machineRepository is null.</exception>
        public MachineController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
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
                return false;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(machine.Name))
                return false;

            if (string.IsNullOrWhiteSpace(machine.SerialNumber))
                return false;

            if (string.IsNullOrWhiteSpace(machine.Model))
                return false;

            if (string.IsNullOrWhiteSpace(machine.Manufacturer))
                return false;

            // Validate dates
            if (machine.InstallationDate > DateTime.Today)
                return false;

            if (machine.NextMaintenanceDate < machine.LastMaintenanceDate)
                return false;

            // Check for duplicate serial number
            try
            {
                var existingMachines = await _machineRepository.GetAllMachinesAsync();
                var duplicateSerial = existingMachines.Any(m => 
                    m.SerialNumber.Equals(machine.SerialNumber, StringComparison.OrdinalIgnoreCase) && 
                    m.MachineId != machine.MachineId);
                
                if (duplicateSerial)
                    return false;
            }
            catch
            {
                // If we can't check for duplicates, assume it's valid
                return true;
            }

            return true;
        }
    }
} 