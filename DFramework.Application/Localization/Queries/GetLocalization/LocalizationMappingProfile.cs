using AutoMapper;
using DFramework.Contracts.Localization;
using Microsoft.Extensions.Localization;
    
namespace DFramework.Application.Localization.Queries.GetLocalization
{
    public class LocalizationMappingProfile : Profile
    {
        public LocalizationMappingProfile()
        {
            CreateMap<LocalizedString, LocalizedKeyDto>();
        }
    }
}
