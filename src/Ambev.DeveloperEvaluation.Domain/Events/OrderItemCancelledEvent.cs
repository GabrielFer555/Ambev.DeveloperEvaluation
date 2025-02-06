namespace Ambev.DeveloperEvaluation.Domain.Events
{
	public record OrderItemCancelledEvent(OrderItem orderItem) : IDomainEvent { }
}
