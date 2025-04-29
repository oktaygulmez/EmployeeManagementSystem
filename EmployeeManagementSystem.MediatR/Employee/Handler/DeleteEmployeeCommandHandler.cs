using EmployeeManagementSystem.MediatR.Employee.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Repository;

namespace EmployeeManagementSystem.MediatR.Employee.Handler
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
           // _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");

            await _employeeRepository.DeleteAsync(request.Id);

            return Unit.Value; // başarılı işlem, geri dönüş yok.
        }
    }
    {
    }
}
