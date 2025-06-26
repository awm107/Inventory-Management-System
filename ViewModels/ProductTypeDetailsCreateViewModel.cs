using Microsoft.AspNetCore.Http;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels
{
    public class ProductTypeDetailsCreateViewModel
    {
        public ProductType ProductTypeName { get; set; }
        [Required]
        public bool Status { get; set; }

    }
}
