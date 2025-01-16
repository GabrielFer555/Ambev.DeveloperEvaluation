

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<GetCartByIdQuery, GetCartByIdResult>
	{
		public async Task<GetCartByIdResult> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
		{
			var validator = new GetCartByIdQueryValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			var cart = await repository.GetCartById(request.Id);
			var result = mapper.Map<GetCartByIdResult>(cart);	
			return result;
		}
	}
}
