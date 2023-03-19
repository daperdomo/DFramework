using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Security.Users.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(IStringLocalizer _localizer)
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(_localizer["user.id.empty"]);
            RuleFor(c => c.Fullname).NotEmpty().WithMessage(_localizer["user.fullname.empty"]);
            RuleFor(c => c.Email).EmailAddress().WithMessage(_localizer["user.email.invalid"]);
        }
    }
}
