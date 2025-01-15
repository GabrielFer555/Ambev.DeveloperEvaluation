
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
	public class GetProductsByIdHandler (IProductRespository repository) : IRequestHandler<GetProductsByIdQuery, GetProductsByIdResult>
	{
		public async Task<GetProductsByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
		{
			GetProductByIdQueryValidator validator = new();
			var validBody = await validator.ValidateAsync(query, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors);
			var product = await repository.GetProductByIdAsync(query.Id);
			if (product is null) {
				throw new NotFoundException("Product", query.Id);
			}

			return new GetProductsByIdResult
			{
				Category = product.Category,
				Description = product.Description,
				Id = query.Id,
				Image = product.Image,
				Price = product.Price,
				Rating = new ProductRatingDto(product.ProductRating.Count, product.ProductRating.Rating),
				Title = product.Title
			};
		}
	}
}
