using AutoMapper;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.MediatR.Employee.Command;
using EmployeeManagementSystem.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Employee.Handler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ServiceResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        public UpdateEmployeeCommandHandler(
          IEmployeeRepository employeeRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _employeeRepository.GetByIdAsync(request.Id);
            if (existingEntity == null || existingEntity.IsDeleted == true)
            {
                return ServiceResponse<EmployeeDto>.Return409("With the specified ID could not be found");
            }

            existingEntity.Id = request.Id;
            existingEntity.Name = request.Name;
            existingEntity.SurName = request.SurName;
            existingEntity.EMail = request.EMail;
            existingEntity.Phone = request.Phone;
            existingEntity.Adress = request.Adress;
            existingEntity.DepartmentId = request.DepartmentId;

            _employeeRepository.Update(existingEntity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Update Page have Error");
                return ServiceResponse<EmployeeDto>.Return500();
            }

            return ServiceResponse<EmployeeDto>.ReturnResultWith200(_mapper.Map<EmployeeDto>(existingEntity));
        }
    }
}

