using AutoMapper;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Handler
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ServiceResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
        public UpdateDepartmentCommandHandler(
          IDepartmentRepository departmentRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<UpdateDepartmentCommandHandler> logger)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _departmentRepository.GetByIdAsync(request.Id);
            if (existingEntity == null || existingEntity.IsDeleted == true)
            {
                return ServiceResponse<DepartmentDto>.Return409("With the specified ID could not be found");  
            }

            existingEntity.Id = request.Id;
            existingEntity.DepartmentName = request.DepartmentName;

             _departmentRepository.Update(existingEntity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Update Page have Error");
                return ServiceResponse<DepartmentDto>.Return500();
            }

            return ServiceResponse<DepartmentDto>.ReturnResultWith200(_mapper.Map<DepartmentDto>(existingEntity));
        }
    }
}

