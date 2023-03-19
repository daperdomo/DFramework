using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Security.Users.Commands.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator(IStringLocalizer _localizer)
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(_localizer["user.id.empty"]);
        }
    }
}
