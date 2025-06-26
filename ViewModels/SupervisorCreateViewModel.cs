using Microsoft.AspNetCore.Http;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels
{
    public class SupervisorCreateViewModel
    {
        [StringLength(450)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid email format")]
        [Display(Name = "Supervisor Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "CNIC cannot exceed 15 characters")]
        [RegularExpression(@"^\d{5}-\d{7}-\d{1}$", ErrorMessage =
            "Invalid CNIC format. Expected format: XXXXX-XXXXXXX-X")]
        [Display(Name = "Supervisor CNIC")]
        public string CNIC { get; set; }

        [Required]
        public GenderType? Gender { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }
        public IFormFile Photo { get; set; }

    }
}
