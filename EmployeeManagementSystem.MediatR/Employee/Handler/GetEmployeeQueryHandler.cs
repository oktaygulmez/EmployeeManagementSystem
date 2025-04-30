using AutoMapper;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.MediatR.Employee.Command;
using EmployeeManagementSystem.Repository;
using MediatR;

namespace EmployeeManagementSystem.MediatR.Employee.Handler
{
    public class GetEmployeeCommandHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeCommandHandler(
         IEmployeeRepository employeeRepository,
          IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _employeeRepository.GetEmployeeById(request.Id);
            var dtos = _mapper.Map<EmployeeDto>(entities);

            return dtos;
        }

    }
}
