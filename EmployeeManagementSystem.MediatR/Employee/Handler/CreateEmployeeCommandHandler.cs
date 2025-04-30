using AutoMapper;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.MediatR.Department.Handler;
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
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ServiceResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        public CreateEmployeeCommandHandler(
          IEmployeeRepository employeeRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<CreateEmployeeCommandHandler> logger)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _employeeRepository.All.FirstOrDefaultAsync(c => c.EMail == request.EMail);
            if (existingEntity != null)
            {
                return ServiceResponse<EmployeeDto>.Return409("Employee Already Exist.");
            }

            var entity = _mapper.Map<EmployeeManagementSystem.Data.Entities.Employee>(request);
            _employeeRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Save Page have Error");
                return ServiceResponse<EmployeeDto>.Return500();
            }

            return ServiceResponse<EmployeeDto>.ReturnResultWith200(_mapper.Map<EmployeeDto>(entity));
        }
    }
}