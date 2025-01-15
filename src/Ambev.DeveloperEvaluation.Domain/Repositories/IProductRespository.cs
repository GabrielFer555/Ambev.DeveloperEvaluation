
namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
	public interface IProductRespository
	{
		Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
		Task<Product> UpdateProductAsync(Product product,int id, CancellationToken cancellationToken);
		Task<IEnumerable<Product>> GetAllProductsAsync(int? page, int? size,  CancellationToken cancellationToken);
		Task<Product?> GetProductByIdAsync(int id);
		Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken);
		Task<List<Product>> GetProductsByCategoriesAsync(string category, int? page, int? size, CancellationToken cancellationToken);
		Task<int> GetTotalPages(int pageSize, string? category);
		Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
	}
}
