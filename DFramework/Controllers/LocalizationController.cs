using DFramework.Application.Authentication.Commands.AuthenticateCommand;
using DFramework.Application.Localization.Queries.GetLocalization;
using DFramework.Contracts.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DFramework.Controllers
{
    public class LocalizationController : ApiControllerBase
    {

        [HttpGet("all")]
        public async Task<Dictionary<string, string>> GetAll()
        {
            return await Mediator.Send(new GetLocalizationQuery());
        }
    }
}