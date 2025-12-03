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
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly CompContext _context;

        public OrderItemRepo(CompContext context)
        {
            _context = context;
        }

        public async Task<OrderItem?> AddAsync(OrderItem orderitem)
        {
            _context.OrderItems.AddAsync(orderitem);
            return await _context.SaveChangesAsync() > 0 ? orderitem : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderitem = _context.OrderItems.FirstOrDefault(oi => oi.Id == id);
            _context.OrderItems.Remove(orderitem);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<OrderItem> GetAllAsync()
        {
            return _context.OrderItems.AsQueryable();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems.SingleOrDefault(oi => oi.Id == id); ;
        }

        public async Task<bool> UpdateAsync(OrderItem orderitem)
        {
            _context.OrderItems.Update(orderitem);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
