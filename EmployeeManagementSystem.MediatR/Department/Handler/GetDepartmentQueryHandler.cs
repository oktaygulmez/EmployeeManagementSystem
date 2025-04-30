using AutoMapper;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.Repository;
using MediatR;

namespace EmployeeManagementSystem.MediatR.Department.Handler
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
    {

        private readonly IDepartmentRepository _depatmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentQueryHandler(
         IDepartmentRepository departmentRepository,
          IMapper mapper)
        {
            _depatmentRepository =departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var entities = await _depatmentRepository.GetDepartmentById(request.Id);
            var dtos = _mapper.Map<DepartmentDto>(entities);

            return dtos;
        }

    }
}