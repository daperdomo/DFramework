using DFramework.Contracts.Security;

namespace DFramework.Application.Security.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
