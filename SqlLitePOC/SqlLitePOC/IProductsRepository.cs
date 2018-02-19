using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlLitePOC
{
    public interface IProductsRepository
    {
		Task<IEnumerable<Product>> GetProductsAsync();
		Task<bool> AddProductsAsync(Product product);
		Task<bool> DeleteProductAsync(int id);
    }
}
