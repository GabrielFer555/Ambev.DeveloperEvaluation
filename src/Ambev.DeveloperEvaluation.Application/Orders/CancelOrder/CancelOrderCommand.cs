namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder
{
	public class CancelOrderCommand:IRequest<CancelOrderResult>
	{
        public int Id { get; set; }
    }
}
