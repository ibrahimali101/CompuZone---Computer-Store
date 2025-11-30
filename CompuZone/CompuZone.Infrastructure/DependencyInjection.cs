using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using CompuZone.Domain.Entities;
using CompuZone.Domain;
using CompuZone.Domain.Interfaces;
using CompuZone.Infrastructure.Repositories;
using CompuZone.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.Infrastructure;
using CompuZone.Infrastructure.UnitOfWorks;
using CompuZone.Domain.Interfaces;
using CompuZone.Domain.Entities;
using CompuZone.Domain;
using CompUZone.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
namespace CompuZone.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository ,  ProductRepository>();
            services.AddScoped<IUnitOfWork ,  UnitOfWork>();
            services.AddIdentity<ApplicaitonUser, IdentityRole>()
               .AddEntityFrameworkStores<CompuZoneContext>();
           
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "mahmoud";
                option.DefaultChallengeScheme = "mahmoud";
            }).AddJwtBearer("mahmoud", builder =>
            {
                string secutirykey = "afslkfskwemwpe,cwpcpwrwepkrcwprkwpecmwesc.f,m/.zcm/f.dzcmf/";
                var securityKeyByte = Encoding.ASCII.GetBytes(secutirykey);
                SecurityKey securityKey = new SymmetricSecurityKey(securityKeyByte);

                builder.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = "http://www.google.com",
                    //ValidAudience = "http://www.google.com",
                    IssuerSigningKey = securityKey
                };
            });

            return services;
        }
    }
}
