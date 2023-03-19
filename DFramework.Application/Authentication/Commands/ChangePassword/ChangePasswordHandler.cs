using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Authentication.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStringLocalizer _localizer;

        public ChangePasswordHandler(
            IDFrameworkDbContext dbContext,
            IPasswordHasher passwordHasher,
            IStringLocalizer localizer)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Username == request.Username);
            if (user == null)
                throw new ArgumentException(_localizer["user.notfound"]);

            var hashedPassword = _passwordHasher.Hash(request.OldPassword);

            if (user.Password.ToLower() == hashedPassword.ToLower())
            {
                hashedPassword = _passwordHasher.Hash(request.NewPassword);
                if (user.Password.ToLower() == hashedPassword.ToLower())
                    throw new ArgumentException(_localizer["newpassword.invalid"]);

                user.Password = _passwordHasher.Hash(request.NewPassword);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Unit.Value;
            }

            throw new ArgumentException(_localizer["password.invalid"]);
        }
    }
}
