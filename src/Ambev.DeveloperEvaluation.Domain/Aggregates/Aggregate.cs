using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
	public abstract class Aggregate:BaseEntity
	{
		private readonly List<IDomainEvent> _domainEvents = new();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
		public IDomainEvent[] DeQueueEvents()
		{
			var DeQueuedEvents = this.DomainEvents.ToArray();
			this._domainEvents.Clear();
			return DeQueuedEvents;
		}
		public void AddEvent(IDomainEvent domainEvent)
		{
			this._domainEvents.Add(domainEvent);
		}
	}
}
