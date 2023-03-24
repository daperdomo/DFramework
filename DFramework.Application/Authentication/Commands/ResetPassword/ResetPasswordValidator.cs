using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IStringLocalizer localizer)
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage(localizer["user.invalid"]);
        }
    }
}