using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.Models
{
    public class AppDbContext : IdentityDbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeName =  "Hugh",  Department = Department.CS, Email=  "hughjackman@mvc.com",PhotoPath="hugh.jpg"},
                new Employee { EmployeeId = 2, EmployeeName = "James",  Department = Department.IT, Email = "jamesbond@mvc.com",  PhotoPath="james.jpg"});
        }
    }
}
