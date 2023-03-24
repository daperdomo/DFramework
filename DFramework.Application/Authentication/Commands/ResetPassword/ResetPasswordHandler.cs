using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace DFramework.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStringLocalizer _localizer;
        private readonly AppSettings _appSettings;

        public ResetPasswordHandler(
            IDFrameworkDbContext dbContext,
            IPasswordHasher passwordHasher,
            IStringLocalizer localizer,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _localizer = localizer;
            _appSettings = appSettings.Value;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Username == request.Username);
            if (user == null)
                throw new ArgumentException(_localizer["user.notfound"]);

            user.Password = _passwordHasher.Hash(_appSettings.DefaultPassword);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
