namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
	public class Cart :  Aggregate<int>
	{
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Products { get; set; }

    }
}
