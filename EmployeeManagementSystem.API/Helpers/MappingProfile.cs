using AutoMapper;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.MediatR.Employee.Command;

namespace EmployeeManagementSystem.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();


            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
