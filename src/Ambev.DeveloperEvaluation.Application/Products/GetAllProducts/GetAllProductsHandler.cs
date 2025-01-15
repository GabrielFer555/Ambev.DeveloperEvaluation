namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsHandler(IProductRespository repository) : IRequestHandler<GetAllProductsQuery, GetAllProductsResult>
	{
		public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			GetAllProductsQueryValidator validator = new();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors);

			int page = request._Page ?? 1;
			int limit = request._Limit ?? 10;
			var listOfProducts = await repository.GetAllProductsAsync(page, limit, cancellationToken);
			var totalPages = await repository.GetTotalPages(limit, null);

			return new GetAllProductsResult
			{
				TotalPages = totalPages,
				Data = listOfProducts,
				Limit = limit,
				Page = page
			};
		}
	}
}
