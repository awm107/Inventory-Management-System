using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> _productList;
        public Product GetProduct(int Id)
        {
            return _productList.FirstOrDefault(e => e.Id == Id);
        }

        //public Product GetProductByClass(int classId)
        //{
        //    return _ProductList.FirstOrDefault(e => e.Class == classId);
        //}
        //public int GetProductClass(int Id)
        //{

        //}

        public IEnumerable<Product> GetAllProducts()
        {
            return _productList;
        }

        public Product Add(Product product)
        {
            product.Id = _productList.Max(e => e.Id) + 1;
            _productList.Add(product);
            return product;
        }

        public Product Update(Product productChanges)
        {
            Product product = _productList.FirstOrDefault(e => e.Id == productChanges.Id);
            if (product != null)
            {
                product.ProductName = productChanges.ProductName;
                product.ProdType = productChanges.ProdType;
                product.Description = productChanges.Description;
                product.ModifiedBy = productChanges.ModifiedBy;
                product.ModifiedDate = productChanges.ModifiedDate;
                product.PurchasedPrice = productChanges.PurchasedPrice;
                product.SalesPrice = product.SalesPrice;
                product.Status = productChanges.Status;
            }
            return product;
        }

        public Product Delete(int id)
        {
            Product product = _productList.FirstOrDefault(e => e.Id == id);
            if (product != null)
            {
                _productList.Remove(product);
            }
            return product;
        }

        public MockProductRepository()
        {
            _productList = new List<Product>
            {
                new Product(){Id=1, ProductName="Mouse"},
                new Product(){Id=2, ProductName="Keyboard"},
                new Product(){Id=3, ProductName="Speaker"}
            };
        }
    }
}
