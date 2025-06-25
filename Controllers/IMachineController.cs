using System.Collections.Generic;
using System.Threading.Tasks;
using goodwin_winForm.Models;

namespace goodwin_winForm.Controllers
{
    public interface IMachineController
    {
        Task<List<Machine>> GetAllMachinesAsync();
        Task<bool> AddMachineAsync(Machine machine);
        Task<bool> UpdateMachineAsync(Machine machine);
        Task<bool> ValidateMachineDataAsync(Machine machine);
    }
} 