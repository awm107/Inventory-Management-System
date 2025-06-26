using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public enum ProductType
    {
        [Display(Name ="Electric")]
        Electric,
        [Display(Name = "Mechanical")]
        Mechanical,
        [Display(Name = "Hardware")]
        Hardware,
        [Display(Name = "Other")]
        Other
    }
}
