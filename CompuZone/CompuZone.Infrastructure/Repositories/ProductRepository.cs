using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using Microsoft.EntityFrameworkCore;
using CompuZone.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product> ,IProductRepository 
    {
        private readonly CompuZoneContext _dbContext;

        public ProductRepository(CompuZoneContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void DeCreaseQuantity(int id , int quantity)
        {
            var product = _dbContext.ProductCatalogs.SingleOrDefault(a => a.ID == id);
            product.QuantityInStock -= quantity;
        }
        public void InCreaseQuantity(int id, int quantity)
        {
            var product = _dbContext.ProductCatalogs.SingleOrDefault(a => a.ID == id);
            product.QuantityInStock += quantity;
        }
        public IQueryable<Product> GetAllWithCategoryAsync()
        {
            return _dbContext.ProductCatalogs.Include(a => a.Category);
        }

        public async Task<Product> GetByIDWithCategoryAsync(int id)
        {
            return await _dbContext.ProductCatalogs.Include(a => a.Category).SingleOrDefaultAsync(a => a.ID == id);
        }

  
    }
}
