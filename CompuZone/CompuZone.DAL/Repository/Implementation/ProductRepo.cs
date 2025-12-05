using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.Interfaces;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.DAL.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly CompContext _context; 

        // Constructor Injection: We ask for the DbContext here
        public ProductRepo(CompContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAllAsync()
        {
            // .Include(p => p.Category) joins the tables so we get Category names too
            return _context.Products
                                 .Include(p => p.Category)
                                 .AsQueryable();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.Images) // Load images too if needed
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
