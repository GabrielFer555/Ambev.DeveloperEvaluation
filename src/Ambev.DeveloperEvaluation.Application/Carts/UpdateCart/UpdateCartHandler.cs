using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
	public class UpdateCartHandler(IMapper mapper, ICartRepository repository, IProductRespository productRepository, IUserRepository userRepository) : IRequestHandler<UpdateCartCommand, UpdateCartResult>
	{
		public async Task<UpdateCartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
		{
			UpdateCartCommandValidator validator = new();
			var isValid = await validator.ValidateAsync(request);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);
			var buyer = await userRepository.GetByIdAsync(request.UserId);
			if (buyer is null )
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
			var cartModel = mapper.Map<Cart>(request);
			var cartUpdated = await repository.UpdateCartAsync(cartModel);
			var result = mapper.Map<UpdateCartResult>(cartUpdated);
			return result;

		}
	}
}
