using AutoMapper;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.MediatR.Department.Command;
using EmployeeManagementSystem.Repository;
using MediatR;

namespace EmployeeManagementSystem.MediatR.Department.Handler
{
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, List<DepartmentDto>>
    {

        private readonly IDepartmentRepository _depatmentRepository;
        private readonly IMapper _mapper;

        public GetAllDepartmentQueryHandler(
         IDepartmentRepository departmentRepository,
          IMapper mapper)
        {
            _depatmentRepository =departmentRepository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var entities = await _depatmentRepository.GetDepartmentAll();
            var dtos = _mapper.Map<List<DepartmentDto>>(entities);

            return dtos;
        }

    }
}