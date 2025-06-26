using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class SQLSalesOfficerRepository : ISalesOfficerRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLSalesOfficerRepository> logger;

        public SQLSalesOfficerRepository(AppDbContext context, ILogger<SQLSalesOfficerRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        SalesOfficer ISalesOfficerRepository.Add(SalesOfficer salesOfficer)
        {
            context.SalesOfficers.Add(salesOfficer);
            context.SaveChanges();
            return salesOfficer;
        }

        SalesOfficer ISalesOfficerRepository.Delete(int id)
        {
            SalesOfficer salesOfficer = context.SalesOfficers.Find(id);
            if (salesOfficer != null)
            {
                context.SalesOfficers.Remove(salesOfficer);
                context.SaveChanges();
            }
            return salesOfficer;

        }

        IEnumerable<SalesOfficer> ISalesOfficerRepository.GetSalesOfficersByUserId(string userId)
        {
            // Ensure this returns a collection, not a single item
            return context.SalesOfficers
                .Where(s => s.UserId == userId)
                .ToList();
        }
        IEnumerable<SalesOfficer> ISalesOfficerRepository.GetAllSalesOfficers()
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return context.SalesOfficers;
        }

        SalesOfficer ISalesOfficerRepository.GetSalesOfficer(int Id)
        {
            return context.SalesOfficers.Find(Id);
        }

        //IEnumerable<SalesOfficer> ISalesOfficerRepository.GetAllSalesOfficersByClass(uint classId)
        //{
        //    return context.SalesOfficers.Where(s => s.Class == classId).ToList();
        //}

        SalesOfficer ISalesOfficerRepository.Update(SalesOfficer salesOfficerChanges)
        {
            var salesOfficer = context.SalesOfficers.Attach(salesOfficerChanges);
            salesOfficer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return salesOfficerChanges;
        }
    }
}
