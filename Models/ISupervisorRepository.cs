using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public interface ISupervisorRepository
    {
        Supervisor GetSupervisor(int id);
        //Supervisor GetSupervisorByClass(int classId);
        //int GetSupervisorClass(int id);
        IEnumerable<Supervisor> GetSupervisorByUserId(string userId);

        IEnumerable<Supervisor> GetAllSupervisors();
        Supervisor Add(Supervisor supervisor);
        Supervisor Update(Supervisor supervisorChanges);
        Supervisor Delete(int id);
    }
}
