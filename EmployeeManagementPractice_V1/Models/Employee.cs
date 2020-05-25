using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }  
        [Required]
        public string EmployeeName { get; set; }  
        public string Email { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
