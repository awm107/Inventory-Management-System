using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "Admin, SalesOfficer")]
    public class SalesOfficerController : Controller
    {
        private readonly ISalesOfficerRepository _SalesOfficerRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public SalesOfficerController(ISalesOfficerRepository SalesOfficerRepository, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _SalesOfficerRepository = SalesOfficerRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        public ViewResult Index()
        {
            var model = _SalesOfficerRepository.GetAllSalesOfficers();
            return View(model);
        }
        //[Authorize(Policy ="AdminRolePolicy")]
        public ViewResult Details(int Id)
        {
            SalesOfficer SalesOfficer = _SalesOfficerRepository.GetSalesOfficer(Id);

            if (SalesOfficer == null)
            {
                Response.StatusCode = 404;
                return View("SalesOfficerNotFound", Id);
            }
            //SalesOfficer model = _SalesOfficerRepository.GetSalesOfficer(Id);
            //ViewBag.PageTitle = "SalesOfficer Details";
            //return View(model);
            SalesOfficerDetailsViewModel SalesOfficerDetailsViewModel = new SalesOfficerDetailsViewModel
            {
                SalesOfficer = _SalesOfficerRepository.GetSalesOfficer(Id),
                PageTitle = "SalesOfficer Details"
            };
            return View(SalesOfficerDetailsViewModel);
            //return View(SalesOfficer);
        }

        private string ProcessUploadFile(SalesOfficerCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filesStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(filesStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        [Authorize(Policy = "AdminCreateRolePolicy")]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "AdminCreateRolePolicy")]
        public async Task<IActionResult> Create(SalesOfficerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                SalesOfficer newSalesOfficer = new SalesOfficer
                {
                    Name = model.Name,
                    Email = model.Email,
                    CNIC = model.CNIC,
                    Address = model.Address,
                    Gender = model.Gender,
                    Photopath = uniqueFileName
                };
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    //ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    newSalesOfficer.UserId = user.Id;
                }
                _SalesOfficerRepository.Add(newSalesOfficer);
                return RedirectToAction("details", new { id = newSalesOfficer.Id });
            }
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "AdminEditRolePolicy")]
        public ViewResult Edit(int id)
        {
            SalesOfficer SalesOfficer = _SalesOfficerRepository.GetSalesOfficer(id);
            SalesOfficerEditViewModel SalesOfficerEditViewModel = new SalesOfficerEditViewModel
            {
                Id = SalesOfficer.Id,
                Name = SalesOfficer.Name,
                //Email = SalesOfficer.Email,
                CNIC = SalesOfficer.CNIC,
                Address = SalesOfficer.Address,
                Gender = SalesOfficer.Gender,
                ExistingPhotoPath = SalesOfficer.Photopath
            };
            return View(SalesOfficerEditViewModel);
        }

        // Through model binding, the action method parameter
        // SalesOfficerEditViewModel receives the posted edit form data
        [HttpPost]
        [Authorize(Policy = "AdminEditRolePolicy")]
        public IActionResult Edit(SalesOfficerEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the SalesOfficer being edited from the database
                SalesOfficer SalesOfficer = _SalesOfficerRepository.GetSalesOfficer(model.Id);
                // Update the SalesOfficer object with the data in the model object
                SalesOfficer.Name = model.Name;
                //SalesOfficer.Email = model.Email;
                SalesOfficer.CNIC = model.CNIC;
                SalesOfficer.Address = model.Address;
                SalesOfficer.Gender = model.Gender;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the SalesOfficer object which will be
                    // eventually saved in the database
                    SalesOfficer.Photopath = ProcessUploadFile(model);
                }

                // Call update method on the repository service passing it the
                // SalesOfficer object to update the data in the database table
                SalesOfficer updatedSalesOfficer = _SalesOfficerRepository.Update(SalesOfficer);

                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "AdminDeleteRolePolicy")]
        public IActionResult Delete(int id)
        {
            SalesOfficer exSalesOfficer = _SalesOfficerRepository.GetSalesOfficer(id);

            if (exSalesOfficer == null)
            {
                return View("SalesOfficerNotFound");
            }
            SalesOfficerDeleteViewModel SalesOfficerDeleteViewModel = new SalesOfficerDeleteViewModel
            {
                Id = exSalesOfficer.Id,
                SalesOfficer = exSalesOfficer,
                PageTitle = "SalesOfficer Delete"
            };
            return View(SalesOfficerDeleteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AdminDeleteRolePolicy")]
        public IActionResult Delete(SalesOfficerDeleteViewModel model)
        {
            SalesOfficer exSalesOfficer = _SalesOfficerRepository.GetSalesOfficer(model.Id);
            if (ModelState.IsValid)
            {
                if (exSalesOfficer == null)
                {
                    Response.StatusCode = 404;
                    return View("SalesOfficerNotFound", exSalesOfficer.Id);
                }

                // Delete the SalesOfficer's photo if it exists
                if (exSalesOfficer.Photopath != null)
                {
                    string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", exSalesOfficer.Photopath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // Delete the SalesOfficer from the repository
                _SalesOfficerRepository.Delete(exSalesOfficer.Id);

                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
