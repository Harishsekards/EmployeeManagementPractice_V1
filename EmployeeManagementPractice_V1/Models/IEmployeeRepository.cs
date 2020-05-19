using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.Models
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public Employee UpdateEmployee(Employee updatedEmployee);
    }
}
