using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels
{
    public class ProductDeleteViewModel
    {
        public Product Product { get; set; }

        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
