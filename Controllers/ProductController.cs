using InventoryManagement.Models;
using InventoryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ProductController(IProductRepository productRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public ViewResult Index()
        {
            var model = _productRepository.GetAllProducts();
            return View(model);
        }
        public ViewResult Inactive()
        {
            var model = _productRepository.GetAllProducts();
            return View(model);
        }
        public ViewResult Details(int Id)
        {
            Product Product = _productRepository.GetProduct(Id);

            if (Product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", Id);
            }
            //Product model = _ProductRepository.GetProduct(Id);
            //ViewBag.PageTitle = "Product Details";
            //return View(model);
            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel
            {
                Product = _productRepository.GetProduct(Id),
                PageTitle = "Product Details"
            };
            return View(productDetailsViewModel);
            //return View(Product);
        }

        //private string ProcessUploadFile(ProductCreateViewModel model)
        //{
        //    string uniqueFileName = null;
        //    if (model.Photo != null)
        //    {
        //        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var filesStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.Photo.CopyTo(filesStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}

        [HttpGet]
        [Authorize(Policy = "CreateProductRolePolicy")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateProductRolePolicy")]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    ProductName = model.ProductName,
                    Description = model.Description,
                    PurchasedPrice = model.PurchasedPrice,
                    SalesPrice = model.SalesPrice,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = System.DateTime.Now,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedDate = System.DateTime.Now,
                    ProdType = model.ProdType,
                    Quantity = model.Quantity,
                    Status = model.Status
                };
                
                _productRepository.Add(newProduct);
                return RedirectToAction("details", new { id = newProduct.Id });
            }
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "EditProductRolePolicy")]
        public ViewResult Edit(int id)
        {
            Product product = _productRepository.GetProduct(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            ProductEditViewModel productEditViewModel = new ProductEditViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                PurchasedPrice = product.PurchasedPrice,
                SalesPrice = product.SalesPrice,
                ModifiedBy = product.ModifiedBy,
                CreatedBy = product.CreatedBy,
                ModifiedDate = product.ModifiedDate,
                ProdType = product.ProdType,
                Quantity = product.Quantity,
                Status = product.Status
            };
            return View(productEditViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditProductRolePolicy")]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the Product being edited from the database
                    Product product = _productRepository.GetProduct(model.Id);
                    product.ProductName = model.ProductName;
                    product.Description = model.Description;
                    product.PurchasedPrice = model.PurchasedPrice;
                    product.SalesPrice = model.SalesPrice;
                    product.ModifiedBy = model.ModifiedBy;
                    product.ModifiedDate = System.DateTime.Now;
                    product.ProdType = model.ProdType;
                    product.Quantity = model.Quantity;
                    product.Status = model.Status;

                    // Call update method on the repository service passing it the
                    // Product object to update the data in the database table
                    Product updatedProduct = _productRepository.Update(product);

                    return RedirectToAction("index");
                }
                
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult Delete(int id)
        {
            Product exProduct = _productRepository.GetProduct(id);

            if (exProduct == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            ProductDeleteViewModel ProductDeleteViewModel = new ProductDeleteViewModel
            {
                Id = exProduct.Id,
                Product = exProduct,
                PageTitle = "Product Delete"
            };
            return View(ProductDeleteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult Delete(ProductDeleteViewModel model)
        {
            Product exProduct = _productRepository.GetProduct(model.Id);
            if (ModelState.IsValid)
            {
                if (exProduct == null)
                {
                    Response.StatusCode = 404;
                    return View("ProductNotFound", exProduct.Id);
                }

               // Delete the Product from the repository
                _productRepository.Delete(exProduct.Id);

                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
