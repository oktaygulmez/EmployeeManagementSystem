using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManagementSystem.Repository
{
    public class DepartmentRepository : GenericRepository<Department, ApplicationDbContext>, IDepartmentRepository
    {

        public DepartmentRepository(IUnitOfWork<ApplicationDbContext> uow) : base(uow)
        {
  
        }

        public async Task<List<DepartmentDto>> GetDepartmentAll()
        {
            var departments = await _context.Departments.Where(x => x.IsDeleted == false)
         .Select(e => new DepartmentDto
         {
             Id = e.Id,
             DepartmentName = e.DepartmentName,
         })
         .ToListAsync();

            return departments;
        }

        public async Task<DepartmentDto> GetDepartmentById(Guid id)
        {
            var department = await _context.Departments.Where(x => x.IsDeleted == false)
        .Where(e => e.Id == id)
        .Select(e => new DepartmentDto
        {
            Id = e.Id,
            DepartmentName = e.DepartmentName,
        })
        .FirstOrDefaultAsync();

            return department;
        }

    }
    
}
