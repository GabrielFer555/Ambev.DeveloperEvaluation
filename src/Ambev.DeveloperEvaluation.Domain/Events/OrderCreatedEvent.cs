﻿namespace Ambev.DeveloperEvaluation.Domain.Events
{
	public record OrderCreatedEvent(Order order) : IDomainEvent { }
}
