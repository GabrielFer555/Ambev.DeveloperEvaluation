namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
	public class GetProductsByIdQuery:IRequest<GetProductsByIdResult>
	{
        public int Id { get; set; }
    }
}
