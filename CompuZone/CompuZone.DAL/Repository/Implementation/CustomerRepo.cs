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
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CompContext _context;
        DbSet<Customer> db;
        public CustomerRepo(CompContext _context) {
            _context = _context;
            this.db = _context.Customers;
        }
        public async Task<PagedList<Customer>> GetPagedAsync(PaginationParams pParams)
        {
            IQueryable<Customer> query = db;
            int totalCount = await query.CountAsync();
            var items = await query
                .Skip((pParams.PageNumber - 1) * pParams.PageSize)
                .Take(pParams.PageSize)
                .ToListAsync();
            return new PagedList<Customer>(items, totalCount, pParams.PageNumber, pParams.PageSize);
        }
        public async Task<Customer?> AddAsync(Customer customer)
        {
            _context.Customers.AddAsync(customer);

            return await _context.SaveChangesAsync() > 0 ? customer : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _context.Customers.Remove(_context.Customers.SingleOrDefault(a => a.CustomerID == id)!);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Customer> GetAllAsync()
        {
            return _context.Customers.Include(a => a.Orders).AsQueryable();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.CustomerID == id);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
