using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.DTOs.AdminUser;
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
    public class AdminUserRepository : GenericRepository<AdminUser, ApplicationDbContext>, IAdminUserRepository
    {

        public AdminUserRepository(IUnitOfWork<ApplicationDbContext> uow) : base(uow)
        {


        }

        public async Task<AdminUser> GetAdminUser(string mail)
        {
            var user = await _context.AdminUsers
                .Where(x => x.EMail == mail)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
