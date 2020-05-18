using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementPractice_V1.Models;
using EmployeeManagementPractice_V1.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementPractice_V1.controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
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
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath,
                Department = employee.Department
            };
            return View(editEmployeeViewModel);
        }


    }
}