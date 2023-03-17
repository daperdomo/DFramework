using DFramework.Application.Common.Models;
using DFramework.Application.Security.Users.Queries.GetAll;
using DFramework.Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFramework.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {

        [HttpGet("all")]
        public async Task<PaginatedList<UserDto>> GetAll([FromQuery] GetAllQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}