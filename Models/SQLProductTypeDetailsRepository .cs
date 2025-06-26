using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLProductRepository> logger;

        public SQLProductRepository(AppDbContext context, ILogger<SQLProductRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        Product IProductRepository.Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        Product IProductRepository.Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;

        }

        IEnumerable<Product> IProductRepository.GetAllProducts()
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return context.Products;
        }

        Product IProductRepository.GetProduct(int Id)
        {
            return context.Products.Find(Id);
        }

        //Product IProductRepository.GetProductByClass(int classId)
        //{
        //    return context.Products.Find(classId);
        //}

        Product IProductRepository.Update(Product productChanges)
        {
            var product = context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }
    }
}
