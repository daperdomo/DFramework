using DFramework.Application.Authentication.Commands.AuthenticateCommand;
using DFramework.Application.Localization.Queries.GetLocalization;
using DFramework.Contracts.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DFramework.Controllers
{
    public class LocalizationController : ApiControllerBase
    {

        [HttpGet("localization/all")]
        public async Task<IEnumerable<LocalizedKeyDto>> GetAll()
        {
            return await Mediator.Send(new GetLocalizationQuery());
        }
    }
}