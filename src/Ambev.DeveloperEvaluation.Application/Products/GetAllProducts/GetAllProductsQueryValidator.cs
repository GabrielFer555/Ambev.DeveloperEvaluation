namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsQueryValidator:AbstractValidator<GetAllProductsQuery>
	{
		public GetAllProductsQueryValidator()
		{
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
		}
	}
}
