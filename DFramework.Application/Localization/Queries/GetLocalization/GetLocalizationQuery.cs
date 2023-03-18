using DFramework.Contracts.Localization;
using MediatR;

namespace DFramework.Application.Localization.Queries.GetLocalization
{
    public class GetLocalizationQuery : IRequest<Dictionary<string, string>>
    {
    }
}
