using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdResult
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public List<OrderItemResponseDto> Items { get; set; } = new();
	}
}
