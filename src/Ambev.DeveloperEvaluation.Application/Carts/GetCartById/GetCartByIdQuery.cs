namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdQuery:IRequest<GetCartByIdResult>
	{
        public int Id { get; set; }
    }
}
