namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
	public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
	{
		private readonly IProductRespository _repository;
		public CreateProductHandler(IProductRespository repository) {
			_repository = repository;
		}
		public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			CreateProductCommandValidator validator = new();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors);

			Product product = CreateNewProduct(request);
			var createdProduct = await _repository.CreateProductAsync(product, cancellationToken);

			return new CreateProductResult
			{
				Id = createdProduct.Id,
				Category = createdProduct.Category,
				Description = createdProduct.Description,
				Image = createdProduct.Image,
				Price = createdProduct.Price,
				Rating = new ProductRatingDto(createdProduct.ProductRating.Count, createdProduct.ProductRating.Rating),
				Title = createdProduct.Title,
			};
		}
		public Product CreateNewProduct(CreateProductCommand request)
		{
			Product product = Product.Create(request.Price, request.Title, request.Description, request.Category, request.Image,
				ProductRating.Of(request.Rating.Count, request.Rating.Rating));
			return product;

		}
	}
}
