namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdQuery:IRequest<GetOrderByIdResult>
	{
        public int Id { get; set; }
    }
}
