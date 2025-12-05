using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Data;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;

namespace CompuZone.DAL.Repository.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly CompContext _context;
        public PaymentRepo(CompContext context)
        {
            _context = context;
        }
        public async Task<Payment?> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return await _context.SaveChangesAsync() > 0 ? payment : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = _context.Payments.SingleOrDefault(p => p.OrderID == id);
            _context.Payments.Remove(payment!);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Payment> GetAllAsync()
        {
            return _context.Payments.AsQueryable();
        }

        public Task<Payment?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
