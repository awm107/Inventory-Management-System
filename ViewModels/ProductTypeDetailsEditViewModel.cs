using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels
{
    public class ProductTypeDetailsEditViewModel : ProductTypeDetailsCreateViewModel 
    { 
        public int Id { get; set; }
    }
}
