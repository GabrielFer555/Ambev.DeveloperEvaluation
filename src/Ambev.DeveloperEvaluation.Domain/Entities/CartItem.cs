namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class CartItem
	{
        public int Id { get; set; }
        public int CartId { get;  set; }
        public int ProductId { get; set; }
		public int Quantity { get; private set; }		
		public CartItem() { }


    }
}
