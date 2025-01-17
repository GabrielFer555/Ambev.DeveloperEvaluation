using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Orders.EventHandlers
{
	public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
	{
		public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{
			logger.LogInformation("Domain Event handled {notification}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
