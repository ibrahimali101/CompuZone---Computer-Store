using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.Interfaces;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task CreateProductAsync(CUProductDto dto);
    }
}
