using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetAllProducts();
        //IEnumerable<SalesOfficer> GetAllSalesOfficersByClass(uint classId);

        Product Add(Product product);
        Product Update(Product productChanges);
        Product Delete(int id);
    }
}
