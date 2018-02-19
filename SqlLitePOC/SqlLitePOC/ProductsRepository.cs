using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlLitePOC
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly DatabaseContext _databaseContext;
		public ProductsRepository(string dbPath)
		{
			_databaseContext = new DatabaseContext(dbPath);
		}
		public async Task<bool> AddProductsAsync(Product product)
		{
			try
			{
				var tracking = _databaseContext.Products.AddAsync(product);
				await _databaseContext.SaveChangesAsync();
				var isAdded = tracking.Status == TaskStatus.RanToCompletion;
				return isAdded;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> DeleteProductAsync(int id)
		{
			try
			{
				var product = await _databaseContext.Products.FindAsync(id);
				var tracking = _databaseContext.Remove(product);
				await _databaseContext.SaveChangesAsync();
				var isDeleted = tracking.State == EntityState.Detached;
				return isDeleted;
			}
			catch
			{
				return false;
			}
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			try
			{
				var products = await _databaseContext.Products.ToListAsync();
				return products;
			}
			catch
			{
				return null;
			}
		}
	}
}
