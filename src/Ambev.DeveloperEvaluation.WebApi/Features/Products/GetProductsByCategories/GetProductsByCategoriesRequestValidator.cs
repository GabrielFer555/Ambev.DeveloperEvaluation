
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesRequestValidator:AbstractValidator<GetProductsByCategoriesRequest>
	{
		public GetProductsByCategoriesRequestValidator() { 
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category Must be informed");
		}	
	}
}
