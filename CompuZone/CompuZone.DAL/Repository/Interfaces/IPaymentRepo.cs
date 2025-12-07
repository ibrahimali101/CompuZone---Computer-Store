using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface IPaymentRepo
    {
        public IQueryable<Payment> GetAllAsync();
        public Task<Payment?> GetByIdAsync(int id);
        public Task<Payment?> AddAsync(Payment payment);
        public Task<bool> UpdateAsync(Payment payment);
        public Task<bool> DeleteAsync(int id);

        public Task<PagedList<Payment>> GetPagedAsync(PaginationParams pParams);
    }
}
