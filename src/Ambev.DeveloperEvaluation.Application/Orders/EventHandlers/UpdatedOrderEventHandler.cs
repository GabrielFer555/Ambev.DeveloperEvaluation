namespace Ambev.DeveloperEvaluation.Application.Orders.EventHandlers
{
	public class UpdatedOrderEventHandler (ILogger<UpdatedOrderEventHandler> logger): INotificationHandler<UpdatedOrderEvent>
	{
		public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
		{
			logger.LogInformation("Domain Event handled {notification}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
