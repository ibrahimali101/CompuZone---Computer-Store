using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum UserRoleOptions
{
    Admin,
    Customer
}


namespace CompuZone.BLL.DTOs.Auth
{
    public class RegisterDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }

        public int Role { get; set; } 

    }
}
