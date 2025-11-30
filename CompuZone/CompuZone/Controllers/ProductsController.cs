using CompuZone.Application.Features.Commands.ProductCommands;
using CompuZone.Application.Features.Queries.ProductQueries;
using CompUZone.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : AppBaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetProductsQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Create(ProductAddCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateCommand command)
        {
            if (id != command.ID)
            {
                return BadRequest("Product ID mismatch");
            }
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{id}")]
    }
}