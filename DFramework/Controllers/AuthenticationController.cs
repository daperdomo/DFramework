using DFramework.Application.Authentication.Commands.AuthenticateCommand;
using DFramework.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DFramework.Controllers
{
    [Route("api/")]
    public class AuthenticationController : ApiControllerBase
    {

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            return Ok(await Mediator.Send(Mapper.Map<AuthenticateCommand>(request)));
        }
    }
}