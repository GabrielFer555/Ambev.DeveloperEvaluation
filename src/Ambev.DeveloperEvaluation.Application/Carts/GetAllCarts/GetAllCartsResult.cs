namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts
{
	public class GetAllCartsResult
	{
		public List<Cart> Data { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
