using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesHandler(IProductRespository repository) : IRequestHandler<GetProductsByCategoriesQuery, GetProductsByCategoriesResult>
	{
		public async Task<GetProductsByCategoriesResult> Handle(GetProductsByCategoriesQuery request, CancellationToken cancellationToken)
		{
			GetProductsByCategoriesQueryValidator validator = new();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors); 

			int page = request._Page ?? 1;
			int limit = request._Limit ?? 10;
			var listOfProducts = await repository.GetProductsByCategoriesAsync(request.Category,page, limit, cancellationToken);
			var totalPages = await repository.GetTotalPages(limit, request.Category);
			return new GetProductsByCategoriesResult
			{
				Limit = limit,
				TotalPages = totalPages,
				Page = page,
				Products = listOfProducts
			};
		}
	}
}
