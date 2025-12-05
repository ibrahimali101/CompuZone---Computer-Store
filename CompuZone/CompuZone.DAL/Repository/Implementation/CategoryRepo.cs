using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.DAL.Repository.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        CompContext _context;
        public CategoryRepo(CompContext context) { 
            _context = context;
        }
        public async Task<Category?> AddAsync(Category category)
        {
            _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync() > 0 ? category : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Category category = _context.Categories.SingleOrDefault(a => a.CategoryID == id)!;
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Category> GetAllAsync()
        {
            return _context.Categories.Include(c => c.Products).AsQueryable();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.CategoryID == id);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
