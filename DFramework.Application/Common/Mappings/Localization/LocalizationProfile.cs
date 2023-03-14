﻿using AutoMapper;
using DFramework.Domain.Entities;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Common.Mappings.Localization
{
    public class LocalizationProfile : Profile
    {
        public LocalizationProfile()
        {
            CreateMap<LanguageResource, LocalizedString>();
        }
    }
}
