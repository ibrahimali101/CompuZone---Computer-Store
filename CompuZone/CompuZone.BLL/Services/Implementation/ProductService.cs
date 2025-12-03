using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Interfaces;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Implementation;

namespace CompuZone.BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _prepo;

        public ProductService(IProductRepository repository)
        {
            _prepo = repository;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products =  _prepo.GetAllAsync();

            // MANUAL MAPPING (Entity -> DTO)
            // Later, you can use AutoMapper to make this cleaner.
            return products.Select(p => new ProductDto
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                CategoryName = p.Category == null ? "Uncategorized" : p.Category.CategoryName, // Null check
                IsInStock = p.QuantityInStock > 0
            }).ToList();
        }

        public async Task<ResponseDto<ProductDto>> CreateProductAsync(CUProductDto dto)
        {
            // Ibrahim: we should validate that the product does not exist.

            // 2. Map DTO -> Entity
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                CategoryID = dto.CategoryID,
                QuantityInStock = dto.QuantityInStock
            };
            // 3. Call Repository
            Product prod = await _prepo.AddAsync(productDT);
            if ()

        }
        public async Task<Response> UpdateProductAsync(CUProductDto dto)
        {
            
        }
    }
}
