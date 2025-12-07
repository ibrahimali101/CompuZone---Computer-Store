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
    public class CategoryRepo : ICategoryRepo
    {
        CompContext _context;
        DbSet<Category> db;
        public CategoryRepo(CompContext context) { 
            _context = context;
            db = _context.Categories;
        }

        public async Task<PagedList<Category>> GetPagedAsync(PaginationParams pParams)
        {
            IQueryable<Category> query = db;
            int totalCount = await query.CountAsync();
            var items = await query
                .Skip((pParams.PageNumber - 1) * pParams.PageSize)
                .Take(pParams.PageSize)
                .ToListAsync();
            return new PagedList<Category>(items, totalCount, pParams.PageNumber, pParams.PageSize);
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
            return _context.Categories.Include(c => c.Products).ThenInclude(c => c.Images).AsQueryable();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
        .Include(c => c.Products)               
            .ThenInclude(p => p.Images)        
        .SingleOrDefaultAsync(c => c.CategoryID == id);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
