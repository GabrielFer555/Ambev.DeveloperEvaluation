using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler (ICartRepository repository, IMapper mapper, IUserRepository userRepository, IProductRespository productRepository) : IRequestHandler<CreateCartCommand, CreateCartResult>
	{
		public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateCartCommandValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);
			var buyer = await userRepository.GetByIdAsync(request.UserId);
			if (buyer is null)
			{
				throw new BadRequestException("UserId is invalid");
			}

			foreach (var product in request.Products)
			{
				var productValid = await productRepository.GetProductByIdAsync(product.ProductId);
				if (productValid is null)
				{
					throw new BadRequestException("Cart has an invalid product");
				}
			}


			var cart = mapper.Map<Ambev.DeveloperEvaluation.Domain.Aggregates.Cart>(request);
			var createdCart = await repository.CreateCartAsync(cart);
			var result = mapper.Map<CreateCartResult>(createdCart);
			return result;
		}
	}
}
