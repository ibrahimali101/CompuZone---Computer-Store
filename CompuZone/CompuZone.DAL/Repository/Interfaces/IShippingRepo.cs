using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface IShippingRepo
    {
        public IQueryable<Shipping> GetAllAsync();
        public Task<Shipping?> GetByIdAsync(int id);
        public Task<Shipping?> AddAsync(Shipping shipping);
        public Task<bool> UpdateAsync(Shipping shipping);
        public Task<bool> DeleteAsync(int id);

        public Task<PagedList<Shipping>> GetPagedAsync(PaginationParams pParams);
    }
}
