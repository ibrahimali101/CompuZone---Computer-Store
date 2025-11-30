using Microsoft.Extensions.DependencyInjection;
using CompuZone.Application.Features.Commands;
using CompuZone.Application.Features.Commands.CategoryCommands;
using CompuZone.Application.Localization;
using CompuZone.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services )
        {
            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(CategoryAddCommand).Assembly,  // Application layer
                    Assembly.GetExecutingAssembly()       // API layer
                );
            });

            services.AddAutoMapper(option => option.AddProfile(new MappingProfile()));
            services.AddSingleton<LocalizationService>();
            services.AddSingleton<SharedLocalizationService>();

            return services;
        }
    }
}
