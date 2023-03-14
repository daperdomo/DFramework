using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DFramework.Application.Authentication.Commands.AuthenticateCommand
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticateCommandHandler(ITokenGenerator tokenGenerator,
            IDFrameworkDbContext dbContext,
            IPasswordHasher passwordHasher)
        {
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Username == request.Username);
            if (user == null)
                throw new ArgumentException("User not found");

            var hashedPassword = _passwordHasher.Hash(request.Password);

            if (user.Password.ToLower() == hashedPassword.ToLower())
            {
                var response = new AuthenticateResponse()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName
                };

                var token = await _tokenGenerator.GenerateToken(response);

                response.Token = token;

                return response;
            }

            throw new ArgumentException("User or password invalid");
        }
    }
}
