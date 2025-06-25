using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetAllMachinesAsync();
        Task<Machine> AddMachineAsync(Machine machine);
        Task<Machine> UpdateMachineAsync(Machine machine);
    }
} 