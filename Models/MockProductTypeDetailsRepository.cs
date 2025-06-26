using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class MockProductTypeDetailsRepository : IProductTypeDetailsRepository
    {
        private List<ProductTypeDetails> _productTypeDetailsList;
        public ProductTypeDetails GetProductTypeDetails(int Id)
        {
            return _productTypeDetailsList.FirstOrDefault(e => e.Id == Id);
        }

        //public Product GetProductByClass(int classId)
        //{
        //    return _ProductList.FirstOrDefault(e => e.Class == classId);
        //}
        //public int GetProductClass(int Id)
        //{

        //}

        public IEnumerable<ProductTypeDetails> GetAllProductTypeDetails()
        {
            return _productTypeDetailsList;
        }

        public ProductTypeDetails Add(ProductTypeDetails productTypeDetails)
        {
            productTypeDetails.Id = _productTypeDetailsList.Max(e => e.Id) + 1;
            _productTypeDetailsList.Add(productTypeDetails);
            return productTypeDetails;
        }

        public ProductTypeDetails Update(ProductTypeDetails productTypeDetailsChanges)
        {
            ProductTypeDetails productTypeDetails = _productTypeDetailsList.FirstOrDefault(e => e.Id == productTypeDetailsChanges.Id);
            if (productTypeDetails != null)
            {
                productTypeDetails.ProductTypeName = productTypeDetailsChanges.ProductTypeName;
                productTypeDetails.Status = productTypeDetailsChanges.Status;
            }
            return productTypeDetails;
        }

        public ProductTypeDetails Delete(int id)
        {
            ProductTypeDetails productTypeDetails = _productTypeDetailsList.FirstOrDefault(e => e.Id == id);
            if (productTypeDetails != null)
            {
                _productTypeDetailsList.Remove(productTypeDetails);
            }
            return productTypeDetails;
        }

        public MockProductTypeDetailsRepository()
        {
            _productTypeDetailsList = new List<ProductTypeDetails>
            {
                new ProductTypeDetails(){Id=1, ProductTypeName=ProductType.Electric},
                new ProductTypeDetails(){Id=2, ProductTypeName=ProductType.Hardware},
                new ProductTypeDetails(){Id=3, ProductTypeName=ProductType.Mechanical}
            };
        }
    }
}
