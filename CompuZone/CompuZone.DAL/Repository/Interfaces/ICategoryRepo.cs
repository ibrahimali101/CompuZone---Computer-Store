using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        public IQueryable<Category> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task<Category?> AddAsync(Category category);
        public Task<bool> UpdateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<PagedList<Category>> GetPagedAsync(PaginationParams pParams);
    }
}
