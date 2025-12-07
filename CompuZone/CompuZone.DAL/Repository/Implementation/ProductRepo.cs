using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.Interfaces;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.DAL.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly CompContext _context; 
        internal DbSet<Product> db;
        public ProductRepo(CompContext context)
        {
            _context = context;
            db = _context.Products;
        }
        public async Task<PagedList<Product>> GetPagedAsync(PaginationParams pParams)
        {
            IQueryable<Product> query = db;
            int totalCount = await query.CountAsync();
            var items = await query
                .Skip((pParams.PageNumber - 1) * pParams.PageSize)
                .Take(pParams.PageSize)
                .ToListAsync();
            return new PagedList<Product>(items, totalCount, pParams.PageNumber, pParams.PageSize);
        }
        public IQueryable<Product> GetAllAsync()
        {
            return _context.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.Images) 
                                 .AsQueryable();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.Images) 
                                 .SingleOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
