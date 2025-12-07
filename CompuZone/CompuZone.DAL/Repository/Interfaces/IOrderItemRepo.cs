using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface IOrderItemRepo
    {
        public IQueryable<OrderItem> GetAllAsync();
        public Task<OrderItem?> GetByIdAsync(int orderid, int productid);
        public Task<OrderItem?> AddAsync(OrderItem orderitem);
        public Task<bool> UpdateAsync(OrderItem orderitem);
        public Task<bool> DeleteAsync(int orderid, int productid);

        public Task<PagedList<OrderItem>> GetPagedAsync(PaginationParams pParams);
    }
}
