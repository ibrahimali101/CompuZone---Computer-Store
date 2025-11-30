using CompuZone.Application.Features.Commands;
using CompuZone.Application.Features.Commands.OrderCommands;
using CompuZone.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        //public async Task<IActionResult> GetByIdAsync(int Id)
        //{
        //    return Ok(await _mediator.Send(new GetOrderByIdQuery { Id = Id }));
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCategoriesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderAddCommand command)
          => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(OrderUpdateCommand command)
         => Ok(await _mediator.Send(command));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> ArchivedAsync(int Id)
        => Ok(await _mediator.Send(new OrderArchivedCommand { ID = Id }));

        [HttpPost("{Id}")]
        public async Task<IActionResult> UnArchivedAsync(int Id)
        => Ok(await _mediator.Send(new OrderUnArchivedCommand { ID = Id }));
    }
}