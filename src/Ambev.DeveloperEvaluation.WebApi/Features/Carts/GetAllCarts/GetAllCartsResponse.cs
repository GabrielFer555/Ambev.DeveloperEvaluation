using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts
{
	public class GetAllCartsResponse
	{
		public List<Cart> Data { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
