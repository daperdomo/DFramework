using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Authentication.Commands.AuthenticateCommand
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage(localizer["user.invalid"]);
            RuleFor(c => c.Password).NotEmpty().WithMessage(localizer["password.invalid"]);
        }
    }
}