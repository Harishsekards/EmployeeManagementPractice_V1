using EmployeeManagementPractice_V1.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.ViewModel
{
    public class EditEmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public string ExistingPhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
