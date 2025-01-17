using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
	public class CreateOrderHandler(IOrdersRepository repository, IProductRespository productRepository, IUserRepository userRepository ,IMapper mapper) : IRequestHandler<CreateOrderCommand, CreateOrderResult>
	{
		public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateOrderCommandValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			foreach (var item in request.Items) { 
				var product = await productRepository.GetProductByIdAsync(item.ProductId);
				if(product is null) throw new BadRequestException($"Product that has Id {item.ProductId} does not exist.");
			}
			var customer = userRepository.GetByIdAsync(request.CustomerId);
			if (customer is null) throw new BadRequestException($"User ({request.CustomerId}) does not exist.");

			var order = mapper.Map<Order>(request);
			var orderCreated = await repository.CreateOrder(order, cancellationToken);
			var result = mapper.Map<CreateOrderResult>(orderCreated);

			return result;
		}
	}
}
