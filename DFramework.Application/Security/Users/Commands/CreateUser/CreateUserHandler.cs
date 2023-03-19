using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Security;
using DFramework.Domain.Entities;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Security.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;
        private readonly IStringLocalizer _localizer;
        public CreateUserHandler(IDFrameworkDbContext dbContext, IMapper mapper, IPasswordHasher hasher, IStringLocalizer localizer)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
            _localizer = localizer;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Users.Any(m => m.Username == request.Username))
                throw new ArgumentException(_localizer["existinguser.message"]);

            var user = _mapper.Map<User>(request);
            user.Active = true;
            user.Password = _hasher.Hash("123456");
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
