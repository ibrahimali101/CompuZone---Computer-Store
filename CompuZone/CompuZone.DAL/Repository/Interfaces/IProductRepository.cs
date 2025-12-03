using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Interfaces
{
    public interface IProductRepository
    {
        public IQueryable<Product> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task<Product?> AddAsync(Product product);
        public Task<bool> UpdateAsync(Product product);
        public Task<bool> DeleteAsync(int id);
    }
}
