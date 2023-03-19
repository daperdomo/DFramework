using DFramework.Application.Common.Models;
using DFramework.Application.Security.Users.Commands.CreateUser;
using DFramework.Application.Security.Users.Commands.DeleteUser;
using DFramework.Application.Security.Users.Commands.UpdateUser;
using DFramework.Application.Security.Users.Queries.GetAll;
using DFramework.Contracts.Security;
using MediatR;
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

        [HttpPost("add")]
        public async Task<CreateUserResponse> Add(CreateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update")]
        public async Task<UpdateUserResponse> Update(UpdateUserCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<Unit> Add([FromRoute] int Id)
        {
            return await Mediator.Send(new DeleteUserCommand
            {
                Id = Id
            });
        }
    }
}