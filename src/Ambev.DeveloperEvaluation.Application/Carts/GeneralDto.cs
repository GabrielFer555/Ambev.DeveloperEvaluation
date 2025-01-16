using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
	public class CartItemDto
	{
        public int ProductId { get; set; }
		public int Quantity { get; set; }
	}

	public class CartItemDtoProfile : Profile
	{
		public CartItemDtoProfile() {
			CreateMap<CartItem, CartItemDto>();

			CreateMap<CartItemDto, CartItem>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(_ => 0))
				.ForMember(dest => dest.CartId, opt => opt.MapFrom(_ => 0));
		}
	}
	public class CartItemDtoValidator : AbstractValidator<CartItemDto>
	{
		public CartItemDtoValidator()
		{
			RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
			RuleFor(x => x.Quantity).NotEmpty();

		}
	}
}
