using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IProductRepository
    {
        public Task<Product> AddProductAsync(Product product);
        public Task<Product> GetProductByIdAsync(Guid id);
        public Task<ICollection<Product>> GetProductAsync();
    }
}
