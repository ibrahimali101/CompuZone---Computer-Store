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
    public class ProductImageRepo : IProductImageRepo
    {
        private readonly CompContext _context;
        public ProductImageRepo(CompContext context)
        {
            _context = context;
        }
        public async Task<ProductImage?> AddAsync(ProductImage productimage)
        {
            _context.ProductImages.AddAsync(productimage);
            return await _context.SaveChangesAsync() > 0 ? productimage : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            ProductImage prod = await _context.ProductImages.SingleOrDefaultAsync(p => p.ImageID == id);

            _context.ProductImages.Remove(prod!);

            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<ProductImage> GetAllAsync()
        {
            return _context.ProductImages.AsQueryable();
        }

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            return await _context.ProductImages.SingleOrDefaultAsync(a => a.ImageID == id);
        }

        public async Task<bool> UpdateAsync(ProductImage productimage)
        {
            _context.ProductImages.Update(productimage);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
