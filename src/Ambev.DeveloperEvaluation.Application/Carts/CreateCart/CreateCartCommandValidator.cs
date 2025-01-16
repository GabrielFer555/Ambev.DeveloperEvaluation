using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Products).NotEmpty().WithMessage("Cart cannot be empty");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
			RuleFor(x => x.Products).ForEach(e => e.SetValidator(new CartItemDtoValidator()));

		}
    }
}
