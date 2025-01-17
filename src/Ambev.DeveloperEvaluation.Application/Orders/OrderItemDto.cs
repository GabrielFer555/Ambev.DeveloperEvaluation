using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders
{
	public class OrderItemResponseDto
	{
		public int ProductId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int Discount { get; set; }
		public decimal TotalPrice => Quantity * Price;
		public decimal TotalDiscount { get; set; }

	}
	public class OrderItemCommandDto
	{
        public int ProductId { get; set; }
        public decimal Price { get; set; }
		public int Quantity { get; set; }
		
		public OrderItemCommandDto() {

		}

	}
	public class OrderItemDtoValidator : AbstractValidator<OrderItemCommandDto>
	{
		public OrderItemDtoValidator()
		{
			RuleFor(x => x.Quantity).LessThanOrEqualTo(20).WithMessage("Cannot have more than 20 equal products in an order");
			RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id must be informed");
			RuleFor(x => x.Quantity).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Quantity must be a valid");
			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(1);
		}
	}

	public class OrderItemDtoProfile : Profile {
		public OrderItemDtoProfile() {
			CreateMap<OrderItemCommandDto, OrderItem>().ConstructUsing(e => new OrderItem(e.ProductId, e.Price, e.Quantity));
			CreateMap<OrderItem, OrderItemResponseDto>();
		}
	}
}
