using CompuZone.Domain.Entities;
using CompUZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Order> Orders { get; }
        IProductRepository Products { get; }

        Task<bool> SaveChangesAsync();
    }
}
