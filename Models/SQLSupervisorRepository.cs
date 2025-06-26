using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class SQLSupervisorRepository : ISupervisorRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLSupervisorRepository> logger;

        public SQLSupervisorRepository(AppDbContext context, ILogger<SQLSupervisorRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        IEnumerable<Supervisor> ISupervisorRepository.GetSupervisorByUserId(string userId)
        {
            // Ensure this returns a collection, not a single item
            return context.Supervisors
                .Where(s => s.UserId == userId)
                .ToList();
        }
        Supervisor ISupervisorRepository.Add(Supervisor supervisor)
        {
            context.Supervisors.Add(supervisor);
            context.SaveChanges();
            return supervisor;
        }

        Supervisor ISupervisorRepository.Delete(int id)
        {
            Supervisor supervisor = context.Supervisors.Find(id);
            if (supervisor != null)
            {
                context.Supervisors.Remove(supervisor);
                context.SaveChanges();
            }
            return supervisor;

        }

        IEnumerable<Supervisor> ISupervisorRepository.GetAllSupervisors()
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return context.Supervisors;
        }

        Supervisor ISupervisorRepository.GetSupervisor(int Id)
        {
            return context.Supervisors.Find(Id);
        }

        //Supervisor ISupervisorRepository.GetSupervisorByClass(int classId)
        //{
        //    return context.Supervisors.Find(classId);
        //}

        Supervisor ISupervisorRepository.Update(Supervisor supervisorChanges)
        {
            var supervisor = context.Supervisors.Attach(supervisorChanges);
            supervisor.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return supervisorChanges;
        }
    }
}
