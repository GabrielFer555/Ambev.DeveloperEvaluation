using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsQueryValidator:AbstractValidator<GetAllProductsQuery>
	{
		public GetAllProductsQueryValidator()
		{
			RuleFor(x => x._Page).Must(IfNotNullMustBePositive);
			RuleFor(x => x._Limit).Must(IfNotNullMustBePositive);

		}
		public bool IfNotNullMustBePositive(int? number)
		{
			if (number.HasValue)
			{
				return number >= 1;
			}
			else
			{
				return true;
			}
		}
	}
}
