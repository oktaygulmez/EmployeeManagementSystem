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
    public interface IAdminUserRepository : IGenericRepository<AdminUser>
    {
        Task<AdminUser> GetAdminUser(string mail);
    }
}
