namespace DFramework.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public string Username { get; set; } = null!;
    }
}
