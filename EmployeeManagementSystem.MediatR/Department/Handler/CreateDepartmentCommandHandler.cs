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

namespace EmployeeManagementSystem.MediatR.Department.Handler
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ServiceResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;
        public CreateDepartmentCommandHandler(
          IDepartmentRepository departmentRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<CreateDepartmentCommandHandler> logger)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _departmentRepository.All.FirstOrDefaultAsync(c => c.DepartmentName == request.DepartmentName);
            if (existingEntity != null)
            {
                return ServiceResponse<DepartmentDto>.Return409("Department Already Exist.");
            }

            var entity = _mapper.Map<EmployeeManagementSystem.Data.Entities.Department>(request);
             _departmentRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Save Page have Error");
                return ServiceResponse<DepartmentDto>.Return500();
            }

            return ServiceResponse<DepartmentDto>.ReturnResultWith200(_mapper.Map<DepartmentDto>(entity));
        }
    }
}