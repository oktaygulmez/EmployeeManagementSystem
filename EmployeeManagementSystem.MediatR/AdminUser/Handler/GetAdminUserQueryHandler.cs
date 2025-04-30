using AutoMapper;
using EmployeeManagementSystem.Data.DTOs.AdminUser;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.MediatR.AdminUser.Command;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.AdminUser.Handler
{
    public class GetAdminUserQueryHandler : IRequestHandler<GetAdminUserQuery, AdminUserDto>
    {

        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;

        public GetAdminUserQueryHandler(
         IAdminUserRepository adminUserRepository,
          IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _mapper = mapper;
        }

        public async Task<AdminUserDto> Handle(GetAdminUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await _adminUserRepository.GetAdminUser(request.Email);
            var dtos = _mapper.Map<AdminUserDto>(entities);

            return dtos;
        }

    }
}