using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;


        }

        public async Task<ResponseDto<Guid>> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description
            };

            var createProduct = await _productRepository.AddProductAsync(product);

            return new ResponseDto<Guid>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = createProduct.Id
            };

        }

        public async Task<ResponseDto<object>> GetProductByIdAsync(Guid id)
        {

            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return new ResponseDto<object>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    ErrorMessage = "Product Id does not found ."
                };
            }

            return new ResponseDto<object>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            };

        }

        public async Task<ResponseDto<ICollection<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetProductAsync();
            return new ResponseDto<ICollection<ProductDTO>>
            {
                IsSuccess = true,
                StatusCode = 200,
                TotalCount = products.Count(),
                Result = products.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }).ToList()
            };
        }
    }
}
