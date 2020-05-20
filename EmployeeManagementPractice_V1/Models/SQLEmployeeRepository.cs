using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLEmployeeRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Employee CreateEmployee(Employee newEmployee)
        {
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
            return newEmployee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dbContext.Employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var model = _dbContext.Employees.Attach(updatedEmployee);
            model.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return updatedEmployee;
        }
    }
}
