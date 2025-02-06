namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders
{
	public class GetAllOrdersHandler(IOrdersRepository repository) : IRequestHandler<GetAllOrdersQuery, GetAllOrdersResult>
	{
		public async Task<GetAllOrdersResult> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
		{
			int page = request._Page ?? 1;
			int limit = request._Limit ?? 10;
			int totalPages = await repository.GetPagesTotal(limit, cancellationToken);
			GetAllOrdersQueryValidator validationRules = new GetAllOrdersQueryValidator();
			var isRequestValid = await validationRules.ValidateAsync(request);
			if (!isRequestValid.IsValid) throw new ValidationException(isRequestValid.Errors);

			var orders = await repository.GetAllOrders(page, limit, cancellationToken);

			return new GetAllOrdersResult
			{
				Items = orders,
				TotalPages = totalPages,
				Limit = limit,
				Page = page
			};
		}
	}
}
