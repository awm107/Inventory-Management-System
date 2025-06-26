using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class ProductTypeDetails
    {
        public int Id { get; set; }
        [Required]
        [Key()]
        public ProductType ProductTypeName { get; set; }
        public string TypeName
        {
            get { return ProductTypeName.ToString(); } // Returns "Electric", "Mechanical", etc.
        }
        [Required]
        public bool Status { get; set; }
    }
}
