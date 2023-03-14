using DFramework.Application.Common.Mappings;
using DFramework.Contracts.Authentication;
using MediatR;

namespace DFramework.Application.Authentication.Commands.AuthenticateCommand
{
    public class AuthenticateCommand : IRequest<AuthenticateResponse>, IMapFrom<AuthenticateRequest>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
