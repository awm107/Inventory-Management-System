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
    [Authorize(Roles = "Admin, Supervisor")]
    public class SupervisorController : Controller
    {
        private readonly ISupervisorRepository _SupervisorRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public SupervisorController(ISupervisorRepository SupervisorRepository, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _SupervisorRepository = SupervisorRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        public ViewResult Index()
        {
            var model = _SupervisorRepository.GetAllSupervisors();
            return View(model);
        }
        //[Authorize(Policy ="AdminRolePolicy")]
        public ViewResult Details(int Id)
        {
            Supervisor Supervisor = _SupervisorRepository.GetSupervisor(Id);

            if (Supervisor == null)
            {
                Response.StatusCode = 404;
                return View("SupervisorNotFound", Id);
            }
            //Supervisor model = _SupervisorRepository.GetSupervisor(Id);
            //ViewBag.PageTitle = "Supervisor Details";
            //return View(model);
            SupervisorDetailsViewModel SupervisorDetailsViewModel = new SupervisorDetailsViewModel
            {
                Supervisor = _SupervisorRepository.GetSupervisor(Id),
                PageTitle = "Supervisor Details"
            };
            return View(SupervisorDetailsViewModel);
            //return View(Supervisor);
        }

        private string ProcessUploadFile(SupervisorCreateViewModel model)
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
        public async Task<IActionResult> Create(SupervisorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                Supervisor newSupervisor = new Supervisor
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
                    newSupervisor.UserId = user.Id;
                }
                _SupervisorRepository.Add(newSupervisor);
                return RedirectToAction("details", new { id = newSupervisor.Id });
            }
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "AdminEditRolePolicy")]
        public ViewResult Edit(int id)
        {
            Supervisor Supervisor = _SupervisorRepository.GetSupervisor(id);
            SupervisorEditViewModel SupervisorEditViewModel = new SupervisorEditViewModel
            {
                Id = Supervisor.Id,
                Name = Supervisor.Name,
                //Email = Supervisor.Email,
                CNIC = Supervisor.CNIC,
                Address = Supervisor.Address,
                Gender = Supervisor.Gender,
                ExistingPhotoPath = Supervisor.Photopath
            };
            return View(SupervisorEditViewModel);
        }

        // Through model binding, the action method parameter
        // SupervisorEditViewModel receives the posted edit form data
        [HttpPost]
        [Authorize(Policy = "AdminEditRolePolicy")]
        public IActionResult Edit(SupervisorEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the Supervisor being edited from the database
                Supervisor Supervisor = _SupervisorRepository.GetSupervisor(model.Id);
                // Update the Supervisor object with the data in the model object
                Supervisor.Name = model.Name;
                //Supervisor.Email = model.Email;
                Supervisor.CNIC = model.CNIC;
                Supervisor.Address = model.Address;
                Supervisor.Gender = model.Gender;

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
                    // PhotoPath property of the Supervisor object which will be
                    // eventually saved in the database
                    Supervisor.Photopath = ProcessUploadFile(model);
                }

                // Call update method on the repository service passing it the
                // Supervisor object to update the data in the database table
                Supervisor updatedSupervisor = _SupervisorRepository.Update(Supervisor);

                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "AdminDeleteRolePolicy")]
        public IActionResult Delete(int id)
        {
            Supervisor exSupervisor = _SupervisorRepository.GetSupervisor(id);

            if (exSupervisor == null)
            {
                return View("SupervisorNotFound");
            }
            SupervisorDeleteViewModel SupervisorDeleteViewModel = new SupervisorDeleteViewModel
            {
                Id = exSupervisor.Id,
                Supervisor = exSupervisor,
                PageTitle = "Supervisor Delete"
            };
            return View(SupervisorDeleteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AdminDeleteRolePolicy")]
        public IActionResult Delete(SupervisorDeleteViewModel model)
        {
            Supervisor exSupervisor = _SupervisorRepository.GetSupervisor(model.Id);
            if (ModelState.IsValid)
            {
                if (exSupervisor == null)
                {
                    Response.StatusCode = 404;
                    return View("SupervisorNotFound", exSupervisor.Id);
                }

                // Delete the Supervisor's photo if it exists
                if (exSupervisor.Photopath != null)
                {
                    string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", exSupervisor.Photopath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // Delete the Supervisor from the repository
                _SupervisorRepository.Delete(exSupervisor.Id);

                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
