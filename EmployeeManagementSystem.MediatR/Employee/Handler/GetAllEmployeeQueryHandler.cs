using AutoMapper;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.MediatR.Employee.Command;
using EmployeeManagementSystem.Repository;
using MediatR;

namespace EmployeeManagementSystem.MediatR.Employee.Handler
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<EmployeeDto>>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeeQueryHandler(
         IEmployeeRepository employeeRepository,
          IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _employeeRepository.GetEmployeeAll();
            var dtos = _mapper.Map<List<EmployeeDto>>(entities);

            return dtos;
        }

    }
}
