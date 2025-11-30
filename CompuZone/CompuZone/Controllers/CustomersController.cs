using CompuZone.Application.Features.Commands.CustomerCommands;
using CompuZone.Application.Features.Queries.CustomerQueries;
using CompUZone.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery { Id = Id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCustomersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CustomerAddCommand command)
          => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CustomerUpdateCommand command)
         => Ok(await _mediator.Send(command));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> ArchivedAsync(int Id)
        => Ok(await _mediator.Send(new CustomerArchivedCommand { ID = Id }));

        [HttpPost("{Id}")]
        public async Task<IActionResult> UnArchivedAsync(int Id)
        => Ok(await _mediator.Send(new CustomerUnArchivedCommand { ID = Id }));
    }
}
