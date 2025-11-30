using MediatR;
using CompuZone.Domain.AccountsDtos;
using CompuZone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.Application.Wapper;

namespace CompuZone.Application.Features.Commands.AccountCommands
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountManager _accountManager;

        public RegisterCommandHandler(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _accountManager.RegisterAsync(new RegisterDto
            {
                UserName = request.UserName,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Email = request.Email
            });

            return new Response<string>(response);
        }
    }
}
