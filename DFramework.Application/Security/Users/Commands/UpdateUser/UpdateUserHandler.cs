using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Security;
using DFramework.Domain.Entities;

namespace DFramework.Application.Security.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IDFrameworkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //var user = _mapper.Map<User>(request);
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
            if (user == null) return null!;

            _mapper.Map(request, user);
            var addedUser = _dbContext.Users.Update(user);

            await _dbContext.SaveChangesAsync();

            return new UpdateUserResponse
            {
                Id = user.Id
            };
        }
    }
}
