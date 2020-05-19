using AutoMapper;
using EmployeeManagementPractice_V1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementPractice_V1.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EditEmployeeViewModel>();
            CreateMap<EditEmployeeViewModel, Employee>();
        }
    }
}
