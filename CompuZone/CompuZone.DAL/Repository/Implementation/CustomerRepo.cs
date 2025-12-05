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
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CompContext context;

        public CustomerRepo(CompContext context) {
            this.context = context;
        }
        public async Task<Customer?> AddAsync(Customer customer)
        {
            context.Customers.AddAsync(customer);

            return await context.SaveChangesAsync() > 0 ? customer : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            context.Customers.Remove(context.Customers.SingleOrDefault(a => a.CustomerID == id)!);
            return await context.SaveChangesAsync() > 0;
        }

        public IQueryable<Customer> GetAllAsync()
        {
            return context.Customers.Include(a => a.Orders).AsQueryable();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await context.Customers.SingleOrDefaultAsync(c => c.CustomerID == id);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            context.Customers.Update(customer);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
