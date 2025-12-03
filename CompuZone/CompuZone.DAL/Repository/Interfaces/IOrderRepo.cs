using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface IOrderRepo
    {
        public IQueryable<Order> GetAllAsync();
        public Task<Order?> GetByIdAsync(int id);
        public Task<Order?> AddAsync(Order order);
        public Task<bool> UpdateAsync(Order order);
        public Task<bool> DeleteAsync(int id);
    }
}
