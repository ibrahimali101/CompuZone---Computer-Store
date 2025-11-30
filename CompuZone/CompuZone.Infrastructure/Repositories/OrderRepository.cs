using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompuZone.Domain.Interfaces;
using CompUZone.Models;

namespace CompuZone.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly CompuZoneContext _dbContext;

        public OrderRepository(CompuZoneContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Returns a single order with its items and product details
        public async Task<Order> GetOrderWithItemsAsync(int id)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Include(o => o.Payment)
                .SingleOrDefaultAsync(o => o.ID == id);
        }

        // Returns all orders for a specific customer, including items
        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}