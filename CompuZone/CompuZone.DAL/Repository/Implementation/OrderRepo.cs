using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.DAL.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly CompContext _context;
        internal DbSet<Order> _orders;
        public OrderRepo(CompContext context)
        {
            _context = context;
            _orders = _context.Orders;
        }
        public async Task<PagedList<Order>> GetPagedAsync(PaginationParams pParams)
        {
            IQueryable<Order> query = _orders;
            int totalCount = await query.CountAsync();
            var items = await query
                .Skip((pParams.PageNumber - 1) * pParams.PageSize)
                .Take(pParams.PageSize)
                .ToListAsync();
            return new PagedList<Order>(items, totalCount, pParams.PageNumber, pParams.PageSize);
        }
        public async Task<Order?> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _context.Orders.Remove(_context.Orders.SingleOrDefault(a => a.OrderID == id)!);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Order> GetAllAsync()
        {
            return _context.Orders.AsQueryable();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.SingleOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
