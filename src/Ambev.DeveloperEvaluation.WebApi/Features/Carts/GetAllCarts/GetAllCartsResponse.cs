
namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts
{
	public class GetAllCartsResponse
	{
		public List<Cart> Data { get; set; } = new();
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
