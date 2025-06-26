using InventoryManagement.Models;
using InventoryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductTypeDetailsController : Controller
    {
        private readonly IProductTypeDetailsRepository _ProductTypeDetailsRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ProductTypeDetailsController(IProductTypeDetailsRepository ProductTypeDetailsRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            _ProductTypeDetailsRepository = ProductTypeDetailsRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public ViewResult Index()
        {
            var model = _ProductTypeDetailsRepository.GetAllProductTypeDetails();
            return View(model);
        }
        public ViewResult Inactive()
        {
            var model = _ProductTypeDetailsRepository.GetAllProductTypeDetails();
            return View(model);
        }
        public ViewResult Details(int Id)
        {
            ProductTypeDetails ProductTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(Id);

            if (ProductTypeDetails == null)
            {
                Response.StatusCode = 404;
                return View("ProductTypeDetailsNotFound", Id);
            }
            ProductTypeDetailsViewModel productTypeDetailsViewModel = new ProductTypeDetailsViewModel
            {
                productTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(Id),
                PageTitle = "ProductTypeDetails Details"
            };
            return View(productTypeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductTypeDetailsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductTypeDetails newProductTypeDetails = new ProductTypeDetails
                {
                    ProductTypeName = model.ProductTypeName,
                    Status = model.Status
                };

                _ProductTypeDetailsRepository.Add(newProductTypeDetails);
                return RedirectToAction("details", new { id = newProductTypeDetails.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ProductTypeDetails ProductTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(id);
            if (ProductTypeDetails == null)
            {
                Response.StatusCode = 404;
                return View("ProductTypeDetailsNotFound", id);
            }
            ProductTypeDetailsEditViewModel ProductTypeDetailsEditViewModel = new ProductTypeDetailsEditViewModel
            {
                Id = ProductTypeDetails.Id,
                ProductTypeName = ProductTypeDetails.ProductTypeName,
                Status = ProductTypeDetails.Status
            };
            return View(ProductTypeDetailsEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductTypeDetailsEditViewModel model)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the ProductTypeDetails being edited from the database
                    ProductTypeDetails ProductTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(model.Id);
                    ProductTypeDetails.ProductTypeName = model.ProductTypeName;
                    ProductTypeDetails.Status = model.Status;

                    // Call update method on the repository service passing it the
                    // ProductTypeDetails object to update the data in the database table
                    ProductTypeDetails updatedProductTypeDetails = _ProductTypeDetailsRepository.Update(ProductTypeDetails);

                    return RedirectToAction("index");
                }

            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult Delete(int id)
        {
            ProductTypeDetails exProductTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(id);

            if (exProductTypeDetails == null)
            {
                Response.StatusCode = 404;
                return View("ProductTypeDetailsNotFound", id);
            }
            ProductTypeDetailsDeleteViewModel ProductTypeDetailsDeleteViewModel = new ProductTypeDetailsDeleteViewModel
            {
                Id = exProductTypeDetails.Id,
                productTypeDetails = exProductTypeDetails,
                PageTitle = "ProductTypeDetails Delete"
            };
            return View(ProductTypeDetailsDeleteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult Delete(ProductTypeDetailsDeleteViewModel model)
        {
            ProductTypeDetails exProductTypeDetails = _ProductTypeDetailsRepository.GetProductTypeDetails(model.Id);
            if (ModelState.IsValid)
            {
                if (exProductTypeDetails == null)
                {
                    Response.StatusCode = 404;
                    return View("ProductTypeDetailsNotFound", exProductTypeDetails.Id);
                }

                // Delete the ProductTypeDetails from the repository
                _ProductTypeDetailsRepository.Delete(exProductTypeDetails.Id);

                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
