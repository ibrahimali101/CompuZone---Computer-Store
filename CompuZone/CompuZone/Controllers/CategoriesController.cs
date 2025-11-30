using CompuZone.Application.Features.Commands;
using CompuZone.Application.Features.Commands.CategoryCommands;
using CompuZone.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery { Id = Id }));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCategoriesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryAddCommand command)
          => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateCommand command)
         => Ok(await _mediator.Send(command));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> ArchivedAsync(int Id)
        => Ok(await _mediator.Send(new CategoryArchivedCommand { ID = Id }));

        [HttpPost("{Id}")]
        public async Task<IActionResult> UnArchivedAsync(int Id)
        => Ok(await _mediator.Send(new CategoryUnArchivedCommand { ID = Id }));
    }
}