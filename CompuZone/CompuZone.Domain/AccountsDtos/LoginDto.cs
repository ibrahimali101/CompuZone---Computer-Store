using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Domain.AccountsDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LanguageRequest Language { get; set; }
    }
}
