using EmployeeManagementSystem.MediatR.Employee.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Repository;
using AutoMapper;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Domain;
using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.MediatR.Employee.Handler;

namespace EmployeeManagementSystem.MediatR.Employee.Handler
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ServiceResponse<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        public DeleteEmployeeCommandHandler(
          IEmployeeRepository employeeRepository,
          IMapper mapper,
          IUnitOfWork<ApplicationDbContext> uow,
          ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _employeeRepository.GetByIdAsync(request.Id);
            if (existingEntity == null || existingEntity.IsDeleted == true)
            {
                return ServiceResponse<bool>.Return409("With the specified ID could not be found");
            }

            _employeeRepository.Delete(existingEntity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Delete Page have Error");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
