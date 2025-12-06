using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CompuZone.DAL.Entities
{
    public class User : IdentityUser
    {
        public DateTime DateJoined { get; set; }

        // Navigation Properties

        public Customer? CustomerProfile { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
