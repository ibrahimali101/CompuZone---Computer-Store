using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CompuZone.DAL.Entities
{
    public class User : IdentityUser
    {
        public DateTime DateJoined { get; set; }
        public Customer? Customer { get; set; }
    }
}
