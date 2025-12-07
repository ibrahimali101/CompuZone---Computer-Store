using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.DAL.Entities;

namespace CompuZone.DAL.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        public IQueryable<Customer> GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id);
        public Task<Customer?> AddAsync(Customer customer);
        public Task<bool> UpdateAsync(Customer customer);
        public Task<bool> DeleteAsync(int id);
        public Task<PagedList<Customer>> GetPagedAsync(PaginationParams pParams);
    }
}
