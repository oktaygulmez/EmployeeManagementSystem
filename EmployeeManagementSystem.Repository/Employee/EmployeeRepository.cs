using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeManagementSystem.Repository
{
    public class EmployeeRepository : GenericRepository<Employee, ApplicationDbContext>, IEmployeeRepository
    {

        public EmployeeRepository(IUnitOfWork<ApplicationDbContext> uow) : base(uow)
        {

        }

        public async Task<List<EmployeeDto>> GetEmployeeAll()
        {
            var employees = await _context.Employees.Where(x => x.IsDeleted == false)
         .Select(e => new EmployeeDto
         {
             Id = e.Id,
             Name = e.Name,
             SurName = e.SurName,
             EMail = e.EMail,
             Phone = e.Phone,
             Adress = e.Adress,
             DepatmentName = e.Department.DepartmentName
         })
         .ToListAsync();

            return employees;
        }

        public async Task<EmployeeDto> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees.Where(x => x.IsDeleted == false)
        .Where(e => e.Id == id)
        .Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            SurName = e.SurName,
            EMail = e.EMail,
            Phone = e.Phone,
            Adress = e.Adress,
            DepatmentName = e.Department.DepartmentName
        })
        .FirstOrDefaultAsync();

            return employee;
        }
    }
}