using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.MediatR.Department.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllDepartmentQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return Ok(await _mediator.Send(new GetDepartmentQuery { Id = id }));
        }

  
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteDepartmentCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

