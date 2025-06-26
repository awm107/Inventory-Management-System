using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class SQLProductTypeDetailsRepository : IProductTypeDetailsRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLProductTypeDetailsRepository> logger;

        public SQLProductTypeDetailsRepository(AppDbContext context, ILogger<SQLProductTypeDetailsRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        ProductTypeDetails IProductTypeDetailsRepository.Add(ProductTypeDetails productTypeDetails)
        {
            context.ProductsTypeDetails.Add(productTypeDetails);
            context.SaveChanges();
            return productTypeDetails;
        }

        ProductTypeDetails IProductTypeDetailsRepository.Delete(int id)
        {
            ProductTypeDetails productTypeDetails = context.ProductsTypeDetails.Find(id);
            if (productTypeDetails != null)
            {
                context.ProductsTypeDetails.Remove(productTypeDetails);
                context.SaveChanges();
            }
            return productTypeDetails;

        }

        IEnumerable<ProductTypeDetails> IProductTypeDetailsRepository.GetAllProductTypeDetails()
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return context.ProductsTypeDetails;
        }

        ProductTypeDetails IProductTypeDetailsRepository.GetProductTypeDetails(int Id)
        {
            return context.ProductsTypeDetails.Find(Id);
        }

        //Product IProductRepository.GetProductByClass(int classId)
        //{
        //    return context.Products.Find(classId);
        //}

        ProductTypeDetails IProductTypeDetailsRepository.Update(ProductTypeDetails productTypeDetailsChanges)
        {
            var productTypeDetails = context.ProductsTypeDetails.Attach(productTypeDetailsChanges);
            productTypeDetails.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productTypeDetailsChanges;
        }
    }
}
