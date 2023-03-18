using AutoMapper;
using DFramework.Contracts.Localization;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Localization.Queries.GetLocalization
{
    public class LocalizationMappingProfile : Profile
    {
        public LocalizationMappingProfile()
        {
            CreateMap<LocalizedString, KeyValuePair<string, string>>()
                .ForMember(d => d.Key, exp => exp.MapFrom(s => s.Name))
                .ForMember(d => d.Value, exp => exp.MapFrom(s => s.Value));
        }
    }
}
