using CompUZone.Models;
using CompuZone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductCatalog>
    {
        Task<ProductCatalog> GetByIDWithCategoryAsync(int id);
        IQueryable<ProductCatalog> GetAllWithCategoryAsync();
        void InCreaseQuantity(int id, int quantity);
        void DeCreaseQuantity(int id, int quantity);
    }
}
