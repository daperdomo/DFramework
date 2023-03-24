using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Security;
using DFramework.Contracts.Settings;
using DFramework.Domain.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace DFramework.Application.Security.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;
        private readonly IStringLocalizer _localizer;
        private readonly AppSettings _appSettings;

        public CreateUserHandler(IDFrameworkDbContext dbContext, IMapper mapper, IPasswordHasher hasher, IStringLocalizer localizer, IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
            _localizer = localizer;
            _appSettings = appSettings.Value;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Users.Any(m => m.Username == request.Username))
                throw new ArgumentException(_localizer["existinguser.message"]);

            var user = _mapper.Map<User>(request);
            user.Active = true;
            user.Password = _hasher.Hash(_appSettings.DefaultPassword);
            user.RolId = 1;

            var addedUser = _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return new CreateUserResponse
            {
                Id = user.Id
            };
        }
    }
}
