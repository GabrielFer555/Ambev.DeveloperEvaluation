using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Orders.EventHandlers
{
	public class CancelledOrderEventHandler(ILogger<CancelledOrderEventHandler> logger) : INotificationHandler<CancelledOrderEvent>
	{
		public Task Handle(CancelledOrderEvent notification, CancellationToken cancellationToken)
		{
			logger.LogInformation("Domain Event handled {notification}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
