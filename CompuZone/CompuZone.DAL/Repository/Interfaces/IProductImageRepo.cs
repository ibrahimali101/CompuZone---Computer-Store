using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface IProductImageRepo
    {
        public IQueryable<ProductImage> GetAllAsync();
        public Task<ProductImage?> GetByIdAsync(int id);
        public Task<ProductImage?> AddAsync(ProductImage productimage);
        public Task<bool> UpdateAsync(ProductImage productimage);
        public Task<bool> DeleteAsync(int id);

        public Task<PagedList<ProductImage>> GetPagedAsync(PaginationParams pParams);
    }
}
