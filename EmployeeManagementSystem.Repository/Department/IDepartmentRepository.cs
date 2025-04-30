using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<List<DepartmentDto>> GetDepartmentAll();
        Task<DepartmentDto> GetDepartmentById(Guid id);
    }
}
