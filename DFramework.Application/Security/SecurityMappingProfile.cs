using AutoMapper;
using DFramework.Contracts.Security;
using DFramework.Domain.Entities;

namespace DFramework.Application.Security
{
    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
