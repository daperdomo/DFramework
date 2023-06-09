﻿using DFramework.Application.Common.Bahaviors;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Application.Services.Authentication;
using DFramework.Application.Services.Localization;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace DFramework.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //Services
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IPasswordHasher, Md5PasswordHasher>();
            services.AddScoped<IStringLocalizer, DbStringLocalizer>();
            services.AddScoped<IUserProfile, UserProfile>();

            return services;
        }
    }
}
