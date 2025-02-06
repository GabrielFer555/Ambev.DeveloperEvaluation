namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
	public class GetAllProductsValidator:AbstractValidator<GetAllProductsRequest>
	{
		public GetAllProductsValidator()
		{
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);

		}
	}
}
