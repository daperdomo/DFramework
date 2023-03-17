using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Models;
using DFramework.Contracts.Security;

namespace DFramework.Application.Security.Users.Queries.GetAll
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, PaginatedList<UserDto>>
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllHandler(IDFrameworkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PaginatedList<UserDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await PaginatedList<UserDto>.CreateAndMapAsync(_dbContext.Users.AsQueryable(),
                request.PageNumber,
                request.PageSize,
                _mapper);
        }
    }
}