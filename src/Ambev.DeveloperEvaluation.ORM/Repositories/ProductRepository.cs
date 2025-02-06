
namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
	public class ProductRepository : IProductRespository
	{	
		private DefaultContext _context;
		public ProductRepository(DefaultContext context) { 
			_context = context;
		}
		public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
		{
			_context.Add(product);
			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<IEnumerable<Product>> GetAllProductsAsync(int? page, int? size, CancellationToken cancellationToken)
		{
			int pageNumber = page ?? 1;
			int pageSize = size ?? 10;
			int itemsToSkip = (pageNumber - 1) * pageSize;
			var productList = await _context.Products.Skip(itemsToSkip).Take(pageSize).ToListAsync(cancellationToken);
			
			return productList;
		}
		public async Task<Product?> GetProductByIdAsync(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
			return product;
		}

		public async Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken)
		{
			var categories = await _context.Products.Select(x => x.Category).Distinct().ToListAsync(cancellationToken);
			return categories;
		}

		public async Task<List<Product>> GetProductsByCategoriesAsync(string category, int? page, int? size, CancellationToken cancellationToken)
		{
			int pageNumber = page ?? 1;
			int pageSize = size ?? 10;
			int itemsToSkip = (pageNumber - 1) * pageSize;
			var productsPerCategory = await _context.Products.Where(e => e.Category == category).Skip(itemsToSkip).Take(pageSize).ToListAsync(cancellationToken);
			return productsPerCategory;
		}

		public async Task<Product> UpdateProductAsync(Product product, int id, CancellationToken cancellationToken)
		{
			var productToBeUpdated = await _context.Products.FirstOrDefaultAsync(x=> x.Id == id);
			if (productToBeUpdated is null)
			{
				throw new NotFoundException("Product", id);
			}
			productToBeUpdated.Update(product.Price, product.Title, product.Description, product.Category, product.Image,
			product.ProductRating);
			await _context.SaveChangesAsync();
			return productToBeUpdated;
			
		}
		public async Task<int> GetTotalPages(int pageSize, string? category )
		{
			var query = _context.Products.AsQueryable();
			if(category is not null)
			{
				query.Where(x => x.Category == category);
			}
			var totalItems = await query.CountAsync();
			var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
			return totalPages;
		
		}

		public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
		{
			var productToBeDeleted = await _context.Products.FirstOrDefaultAsync( x => x.Id == id);
			if(productToBeDeleted is null)
			{
				throw new NotFoundException("Product", id);	
			}
			var hasOrderWithThatProduct = await _context.OrderItems.AnyAsync( x => x.ProductId == productToBeDeleted.Id);
			var hasCartWithThatProduct = await _context.CartItems.AnyAsync(x => x.ProductId == productToBeDeleted.Id);
			if (hasCartWithThatProduct || hasOrderWithThatProduct) {
				throw new BadRequestException("Impossible to delete product that is in order or cart");
			}
			_context.Products.Remove(productToBeDeleted);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
