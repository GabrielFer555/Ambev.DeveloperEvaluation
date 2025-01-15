using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
	public class GetAllProductsValidator:AbstractValidator<GetAllProductsRequest>
	{
		public GetAllProductsValidator()
		{
			RuleFor(x => x._Page).Must(IfNotNullMustBePositive);
			RuleFor(x => x._Limit).Must(IfNotNullMustBePositive);

		}
		public bool IfNotNullMustBePositive(int? number)
		{
			if (number.HasValue) {
				return number >= 1;
			}
			else
			{
				return true;
			}
		}
	}
}
