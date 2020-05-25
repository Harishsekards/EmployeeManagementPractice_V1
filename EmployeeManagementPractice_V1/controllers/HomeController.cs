using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagementPractice_V1.Models;
using EmployeeManagementPractice_V1.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementPractice_V1.controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMapper mapper;

        public HomeController(IEmployeeRepository employeeRepository,IWebHostEnvironment hostEnvironment,IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this.hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {            
            var model = _employeeRepository.GetEmployeeById(id);
            if(model != null)
            {
                return View(model);
            }                            
            return View("NotFound",id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                ViewBag.Title = "Edit Employee";
                Employee employee = _employeeRepository.GetEmployeeById(id);
                EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel();
                mapper.Map(employee, editEmployeeViewModel);
                return View(editEmployeeViewModel);
            }
            ViewBag.Title = "Create Employee";
            EditEmployeeViewModel editEmployeeViewModel_Create = new EditEmployeeViewModel()
            {                
                Department = Department.CS,
                PhotoPath = "noimage.jpg"
            };
            return View(editEmployeeViewModel_Create);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel updatedemployee)
        {
            if (ModelState.IsValid)
            {
                string uniqueName = null;
                if (updatedemployee.Photo != null)
                {
                    uniqueName = ProcessUploadedImage(updatedemployee);
                }
                Employee employee = new Employee();
                mapper.Map(updatedemployee, employee);
                employee.PhotoPath = string.IsNullOrEmpty(uniqueName) ? updatedemployee.PhotoPath : uniqueName;
                if (updatedemployee.EmployeeId != 0)
                {
                    _employeeRepository.UpdateEmployee(employee);
                }
                _employeeRepository.CreateEmployee(employee);
                return RedirectToAction("details", new { id = employee.EmployeeId });
            }
            return View(updatedemployee);
        }

        private string ProcessUploadedImage(EditEmployeeViewModel updatedemployee)
        {
            string uniqueName = null;
            string imagesFolderLocation = Path.Combine(hostEnvironment.WebRootPath, "images");
            uniqueName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(updatedemployee.Photo.FileName);
            string user_Uploaded_Image = Path.Combine(imagesFolderLocation, uniqueName);
            using (var copyFileToServer = new FileStream(user_Uploaded_Image, FileMode.Create))
            {
                updatedemployee.Photo.CopyTo(copyFileToServer);
            }
            return uniqueName;
        }

       

       
    }
}