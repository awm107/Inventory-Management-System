using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public interface IProductTypeDetailsRepository
    {
        ProductTypeDetails GetProductTypeDetails(int id);
        IEnumerable<ProductTypeDetails> GetAllProductTypeDetails();
        //IEnumerable<SalesOfficer> GetAllSalesOfficersByClass(uint classId);

        ProductTypeDetails Add(ProductTypeDetails productTypeDetails);
        ProductTypeDetails Update(ProductTypeDetails productTypeDetailsChanges);
        ProductTypeDetails Delete(int id);
    }
}
