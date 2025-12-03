using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;

namespace CompuZone.DAL.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly CompContext _context;

        public OrderRepo(CompContext context)
        {
            _context = context;
        }
        public async Task<Order?> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _context.Orders.Remove(_context.Orders.SingleOrDefault(id));
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Order> GetAllAsync()
        {
            return _context.Orders.AsQueryable();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
