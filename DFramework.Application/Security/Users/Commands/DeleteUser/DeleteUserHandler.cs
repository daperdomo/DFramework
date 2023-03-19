using AutoMapper;
using DFramework.Application.Common.Interfaces;

namespace DFramework.Application.Security.Users.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteUserHandler(IDFrameworkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
            if (user != null)
            {
                var addedUser = _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return Unit.Value;
        }
    }
}
