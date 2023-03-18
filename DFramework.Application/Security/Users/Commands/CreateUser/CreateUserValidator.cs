using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Security.Users.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(IStringLocalizer _localizer)
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage(_localizer["user.empty"]);
            RuleFor(c => c.Fullname).NotEmpty().WithMessage(_localizer["fullname.empty"]);
            RuleFor(c => c.Email).EmailAddress().WithMessage(_localizer["email.invalid"]);
        }
    }
}
