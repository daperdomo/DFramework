using AutoMapper;
using DFramework.Application.Security.Users.Commands.CreateUser;
using DFramework.Application.Security.Users.Commands.UpdateUser;
using DFramework.Contracts.Security;
using DFramework.Domain.Entities;

namespace DFramework.Application.Security
{
    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>()
                .ForMember(d => d.Password, exp => exp.Ignore())
                .ForMember(d => d.Active, exp => exp.Ignore())
                .ForMember(d => d.RolId, exp => exp.Ignore())
                .ForMember(d => d.CreatedDate, exp => exp.Ignore());
        }
    }
}
