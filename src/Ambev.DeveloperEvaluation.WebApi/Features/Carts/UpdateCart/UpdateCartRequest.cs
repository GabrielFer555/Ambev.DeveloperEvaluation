namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
	public class UpdateCartRequest
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public Guid UserId { get; set; }
		public List<CartItemDto> Products { get; set; } = new();
	}
}
