﻿namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
	public abstract class Aggregate<T>:Entity<T>
	{
		private readonly List<IDomainEvent> _domainEvents = new();
		[JsonIgnore]
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
