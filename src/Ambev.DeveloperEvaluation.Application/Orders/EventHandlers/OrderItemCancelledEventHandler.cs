using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Orders.EventHandlers
{
	public class OrderItemCancelledEventHandler(ILogger<OrderItemCancelledEventHandler> logger) : INotificationHandler<OrderItemCancelledEvent>
	{
		public Task Handle(OrderItemCancelledEvent notification, CancellationToken cancellationToken)
		{
			logger.LogInformation("Domain Event handled {notification}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
