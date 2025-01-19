
namespace Ambev.DeveloperEvaluation.ORM.Interceptors
{
	public class DispatchDomainEventsInterceptor:SaveChangesInterceptor
	{
		private readonly IMediator _mediator;

		public DispatchDomainEventsInterceptor(IMediator mediator)
		{
			_mediator = mediator;
		}
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			DispatchChanges(eventData.Context).GetAwaiter().GetResult();
			return base.SavingChanges(eventData, result);
		}
		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			await DispatchChanges(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public async Task DispatchChanges(DbContext? dbContext)
		{
			if (dbContext == null) return;
			var aggregates = dbContext.ChangeTracker.Entries<Aggregate<int>>().Where(e=> e.Entity.DomainEvents.Any()).Select(e => e.Entity);

			var domainEvents = aggregates.SelectMany(e => e.DomainEvents).ToList();

			aggregates.ToList().ForEach(e => e.DeQueueEvents());
			foreach (var domainEvent in domainEvents) {
				await _mediator.Publish(domainEvent);
			}

		}
	}
}
