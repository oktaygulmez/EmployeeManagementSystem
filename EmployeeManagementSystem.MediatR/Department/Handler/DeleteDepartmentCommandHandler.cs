using AutoMapper;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Handler
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ServiceResponse<bool>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;
        public DeleteDepartmentCommandHandler(
          IDepartmentRepository departmentRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<DeleteDepartmentCommandHandler> logger)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _departmentRepository.GetByIdAsync(request.Id);
            if (existingEntity == null || existingEntity.IsDeleted == true)
            {
                return ServiceResponse<bool>.Return409("With the specified ID could not be found");
            }

            _departmentRepository.Delete(existingEntity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Delete Page have Error");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
