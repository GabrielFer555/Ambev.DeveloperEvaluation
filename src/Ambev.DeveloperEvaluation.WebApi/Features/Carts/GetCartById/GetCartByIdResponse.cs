namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById
{
	public class GetCartByIdResponse
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public Guid UserId { get; set; }
		public List<CartItemDto> Products { get; set; }
	}
}
