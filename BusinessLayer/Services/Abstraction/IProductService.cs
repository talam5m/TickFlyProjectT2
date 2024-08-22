using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstraction
{
    public interface IProductService
    {
        public Task<ResponseDto<Guid>> AddProductAsync(ProductDTO productDto);
        public Task<ResponseDto<object>> GetProductByIdAsync(Guid id);
        public Task<ResponseDto<ICollection<ProductDTO>>> GetAllProductsAsync();
    }
}
