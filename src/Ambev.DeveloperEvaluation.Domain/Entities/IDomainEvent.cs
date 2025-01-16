using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public interface IDomainEvent:INotification
	{
		Guid EventId => Guid.NewGuid();
		public DateTime OccurredOn => DateTime.Now;
		public string EventType => GetType().AssemblyQualifiedName;
	}
}
