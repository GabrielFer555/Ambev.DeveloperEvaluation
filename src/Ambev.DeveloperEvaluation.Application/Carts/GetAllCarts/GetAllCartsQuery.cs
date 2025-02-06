namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts
{
	public class GetAllCartsQuery:IRequest<GetAllCartsResult>
	{
		public int? _Page { get; set; }
		public int? _Limit { get; set; }
	}
}
