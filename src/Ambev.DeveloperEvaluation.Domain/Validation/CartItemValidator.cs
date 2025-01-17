using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
	public class CartItemValidator:AbstractValidator<CartItem>
	{
		public CartItemValidator() { 
			RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
			RuleFor(x => x.Quantity).LessThanOrEqualTo(20);
			RuleFor(x => x.Quantity).NotEmpty();
		}
	}
}
