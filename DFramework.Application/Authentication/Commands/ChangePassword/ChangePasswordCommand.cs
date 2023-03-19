namespace DFramework.Application.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string Username { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmedPassword { get; set; } = null!;
    }
}
