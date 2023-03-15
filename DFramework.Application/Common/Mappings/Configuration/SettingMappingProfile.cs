using AutoMapper;
using DFramework.Contracts.Configuration;
using DFramework.Domain.Entities;

namespace DFramework.Application.Common.Mappings.Configuration
{
    public class SettingMappingProfile : Profile
    {
        public SettingMappingProfile()
        {
            CreateMap<Setting, SettingKeyDto>()
                .ForMember(d => d.Name, s => s.MapFrom(ss => ss.SettingKey))
                .ForMember(d => d.Value, s => s.MapFrom(ss => ss.SettingValue));
        }
    }
}
