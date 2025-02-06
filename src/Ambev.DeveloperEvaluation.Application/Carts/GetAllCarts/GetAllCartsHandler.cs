namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts
{
	internal class GetAllCartsHandler (ICartRepository repository) : IRequestHandler<GetAllCartsQuery, GetAllCartsResult>
	{
		public async Task<GetAllCartsResult> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
		{
			int page = request._Page ?? 1;
			int limit = request._Limit ?? 10;
			int totalPages = await repository.GetTotalPages(limit);
			GetAllCartsValidator validationRules = new GetAllCartsValidator();
			var isRequestValid = await validationRules.ValidateAsync(request);
			if (!isRequestValid.IsValid) throw new ValidationException(isRequestValid.Errors);

			var listOfCarts = await repository.GetAllCarts(page,limit);
			return new GetAllCartsResult
			{
				Data = listOfCarts,
				Limit = limit,
				Page = page,
				TotalPages = totalPages
			};
		}
	}
}
