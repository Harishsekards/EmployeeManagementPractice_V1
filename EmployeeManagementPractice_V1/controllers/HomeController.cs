﻿using System;
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
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel();
            mapper.Map(employee,editEmployeeViewModel);           
            return View(editEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel updatedemployee)
        {
            string uniqueName = null;
            if (updatedemployee.Photo != null)
            {
                uniqueName = ProcessUploadedImage(updatedemployee);
            }
            Employee employee = new Employee();
            mapper.Map(updatedemployee, employee);
            employee.PhotoPath = string.IsNullOrEmpty(uniqueName) ? updatedemployee.PhotoPath : uniqueName;                       
            _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("details",new {id= employee.EmployeeId});
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