using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name ="ProductName")]
        public string ProductName { get; set; }

        [MaxLength(50, ErrorMessage ="Description cannot exceed 50 characters")]
        public string Description { get; set; }
        [Required]
        public double PurchasedPrice { get; set; }
        [Required]
        public double SalesPrice { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        public ProductType ProdType { get; set; }
        public string TypeName
        {
            get { return ProdType.ToString(); } // Returns "Electric", "Mechanical", etc.
        }

        [ForeignKey("ProdType")]
        public ProductTypeDetails ProductTypeName { get; set; }
        public uint Quantity { get; set; }
        [Required]
        public bool Status { get; set; } 
    }
}
