namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
	public class UpdateProductHandler(IProductRespository repository, IMapper mapper): IRequestHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			UpdateProductCommandValidator validator = new();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors);

			var updatedModel = Product.Create(request.Price, request.Title, request.Description, request.Category, request.Image,
				ProductRating.Of(request.Rating.Count, request.Rating.Rating));
			var updatedProduct = await repository.UpdateProductAsync(updatedModel, request.Id, cancellationToken); //its returning null
			var result = mapper.Map<UpdateProductResult>(updatedProduct);
			return result;
		}
	}
}
