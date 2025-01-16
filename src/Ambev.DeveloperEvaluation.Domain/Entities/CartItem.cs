using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class CartItem:Entity<int>
	{
		[JsonIgnore]
        public int CartId { get;  set; }
		[JsonIgnore]
		public int ProductId { get; set; }
		public int Quantity { get;  set; }		
		public CartItem() { }


    }
}
