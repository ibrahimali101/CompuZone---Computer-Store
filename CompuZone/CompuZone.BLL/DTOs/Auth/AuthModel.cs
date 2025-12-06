using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Auth
{
    public class AuthModel
    {
        // Token, Message, IsAuthenticated, Username, Email, Roles
        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
