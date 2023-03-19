using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Authentication.Commands.AuthenticateCommand
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStringLocalizer _localizer;

        public AuthenticateCommandHandler(ITokenGenerator tokenGenerator,
            IDFrameworkDbContext dbContext,
            IPasswordHasher passwordHasher,
            IStringLocalizer localizer)
        {
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _localizer = localizer;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Username == request.Username);
            if (user == null)
                throw new ArgumentException(_localizer["authentication.user.notfound"]);

            var hashedPassword = _passwordHasher.Hash(request.Password);

            if (user.Password.ToLower() == hashedPassword.ToLower())
            {
                var response = new AuthenticateResponse()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    FullName = user.FullName
                };

                var token = await _tokenGenerator.GenerateToken(response);

                response.Token = token;

                return response;
            }

            throw new ArgumentException(_localizer["authentication.notvalid"]);
        }
    }
}
