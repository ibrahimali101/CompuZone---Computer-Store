using System.Security.Cryptography;
using System.Text;
using CompuZone.Application.Features.Commands.AccountCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}