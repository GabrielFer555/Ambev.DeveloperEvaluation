
namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesQueryValidator:AbstractValidator<GetProductsByCategoriesQuery>
	{
		public GetProductsByCategoriesQueryValidator() {
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category Must be informed");
		}
	}
}
