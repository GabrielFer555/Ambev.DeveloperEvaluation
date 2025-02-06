namespace Ambev.DeveloperEvaluation.Domain.Events
{
	public record UpdatedOrderEvent(Order order): IDomainEvent { }
}
