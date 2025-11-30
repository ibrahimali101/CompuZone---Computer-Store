using CompuZone.Domain;
using CompuZone.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using CompuZone.Domain;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string UserId { get => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value; }
        public string UserName { get => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value; }
        public int? StockId { get => Convert.ToInt32( _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "StocKId")?.Value); }
        public LanguageRequest Language 
        {
            get 
            {
                var data  = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "Language").Value;
              
                if (data == "Arabic")
                    return LanguageRequest.Arabic;
                else
                    return LanguageRequest.English;
            }
        }
    }
}
