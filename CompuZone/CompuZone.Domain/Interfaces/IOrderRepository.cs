using CompuZone.Domain.Interfaces;
using CompUZone.Models; // This is where your Order entity is
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompuZone.Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // Get a single order with its related data (OrderItems, Products, etc.)
        Task<Order> GetOrderWithItemsAsync(int id);

        // Get all orders for a specific customer (for "My Orders" history)
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    }
}