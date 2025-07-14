using System.Collections.Generic;
using System.Threading.Tasks;
using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    public interface IMachineController
    {
        // Data access methods only
        Task<List<Machine>> GetAllMachinesAsync();
        Task<Machine?> GetMachineByIdAsync(int machineId);
        Task<bool> AddMachineAsync(Machine machine);
        Task<bool> UpdateMachineAsync(Machine machine);
    }
} 