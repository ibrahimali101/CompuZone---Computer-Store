using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using CompuZone.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompuZoneContext _context;

        public IGenericRepository<Order> Orders { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(
            CompuZoneContext context,
            IGenericRepository<Order> orderRepository,
            IProductRepository productRepository)
        {
            _context = context;
            Orders = orderRepository;
            Products = productRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var rowsAffacted =  await _context.SaveChangesAsync();
            return rowsAffacted > 0;
        }
        public void Dispose()
            => _context.Dispose();
    }

}
