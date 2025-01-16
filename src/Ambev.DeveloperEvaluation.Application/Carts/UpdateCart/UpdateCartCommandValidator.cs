using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
	public class UpdateCartCommandValidator:AbstractValidator<UpdateCartCommand>
	{
		public UpdateCartCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id cannot be empty");
			RuleFor(x => x.Products).NotEmpty().WithMessage("Cart cannot be empty");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
			RuleFor(x => x.Products).ForEach(e => e.SetValidator(new CartItemDtoValidator()));
		}
	}
}
