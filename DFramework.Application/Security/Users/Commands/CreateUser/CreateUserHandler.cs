using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Security;
using DFramework.Domain.Entities;

namespace DFramework.Application.Security.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;
        public CreateUserHandler(IDFrameworkDbContext dbContext, IMapper mapper, IPasswordHasher hasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
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
