using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders
{
	public class GetAllOrdersResponse
	{
		public List<Order> Items { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
