namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdHandler(IMapper mapper, IOrdersRepository repository) : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResult>
	{
		public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var validator = new GetOrderByIdQueryValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			var order = await repository.GetOrderByNumber(request.Id, cancellationToken);
			var result = mapper.Map<GetOrderByIdResult>(order);

			return result;
		}
	}
}
