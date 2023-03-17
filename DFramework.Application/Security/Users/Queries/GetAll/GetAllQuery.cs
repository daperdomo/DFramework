using DFramework.Application.Common.Models;
using DFramework.Contracts.Security;
using MediatR;

namespace DFramework.Application.Security.Users.Queries.GetAll
{
    public class GetAllQuery : IRequest<PaginatedList<UserDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
