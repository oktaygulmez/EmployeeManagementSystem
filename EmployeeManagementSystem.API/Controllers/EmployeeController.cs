using EmployeeManagementSystem.MediatR.Employee.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return Ok(await _mediator.Send(new GetEmployeeQuery { Id = id }));
        }


        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
