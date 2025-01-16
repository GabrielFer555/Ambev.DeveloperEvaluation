namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemDto> Products { get; set; } = new();
    }
}
