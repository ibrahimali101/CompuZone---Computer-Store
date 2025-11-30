using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Localization
{
    public class SharedLocalizationService : LocalizationService
    {
        public SharedLocalizationService(IConfiguration configuration) : base(configuration)
        {
            base.Configure();
        }
    }
}
