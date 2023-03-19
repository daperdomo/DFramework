using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Authentication.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator(IStringLocalizer localizer)
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage(localizer["user.invalid"]);
            RuleFor(c => c.OldPassword).NotEmpty().WithMessage(localizer["password.invalid"]);
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage(localizer["newpassword.invalid"]);
            RuleFor(c => c.ConfirmedPassword).NotEmpty().WithMessage(localizer["confirmedpassword.invalid"]);
            RuleFor(c => c.NewPassword).Must((args, NewPassword) => NewPassword == args.ConfirmedPassword)
                .WithMessage(localizer["confirmedpassword.notmatched"]);
        }
    }
}