using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public interface ISalesOfficerRepository
    {
        SalesOfficer GetSalesOfficer(int id);
        IEnumerable<SalesOfficer> GetAllSalesOfficers();
        //IEnumerable<SalesOfficer> GetAllSalesOfficersByClass(uint classId);

        IEnumerable<SalesOfficer> GetSalesOfficersByUserId(string userId);
        SalesOfficer Add(SalesOfficer salesOfficer);
        SalesOfficer Update(SalesOfficer salesOfficerChanges);
        SalesOfficer Delete(int id);
    }
}
