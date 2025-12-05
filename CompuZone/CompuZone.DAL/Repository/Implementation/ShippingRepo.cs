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
    public class ShippingRepo : IShippingRepo
    {
        private readonly CompContext _context;
        public ShippingRepo(CompContext context) {
            _context = context;
        }

        public async Task<Shipping?> AddAsync(Shipping shipping)
        {
            _context.Shippings.Add(shipping);

            return await _context.SaveChangesAsync() > 0 ? shipping : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _context.Shippings.Remove(_context.Shippings.SingleOrDefault(a => a.ShippingID == id)!);

            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Shipping> GetAllAsync()
        {
            return _context.Shippings.AsQueryable();
        }

        public async Task<Shipping?> GetByIdAsync(int id)
        {
            return await _context.Shippings.SingleOrDefaultAsync(a => a.ShippingID == id);
        }

        public async Task<bool> UpdateAsync(Shipping shipping)
        {
            _context.Shippings.Update(shipping);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
