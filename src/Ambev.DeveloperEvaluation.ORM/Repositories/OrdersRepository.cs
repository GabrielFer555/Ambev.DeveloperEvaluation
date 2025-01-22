using System.Threading;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
	public class OrdersRepository (DefaultContext context): IOrdersRepository
	{
		public async Task<bool> CancelOrder(int id, CancellationToken cancelToken = default)
		{
			var order = await GetOrderByNumber(id);
			order.OrderStatus = Domain.Enums.OrderStatus.Canceled;
			foreach (var item in order.Items) { 
				item.OrderItemStatus = Domain.Enums.OrderItemStatus.Canceled;
			}
			order.AddEvent(new CancelledOrderEvent(id));
			await context.SaveChangesAsync();
			return true;
		}

		public async Task<Order> CancelOrderItem(int orderId, int orderItemId, CancellationToken cancelToken = default)
		{
			var order = await GetOrderByNumber(orderId);
			var orderItem = order.Items.FirstOrDefault(x => x.ProductId == orderItemId);
			orderItem!.OrderItemStatus = OrderItemStatus.Canceled;

			order.AddEvent(new OrderItemCancelledEvent(orderItem));

			await context.SaveChangesAsync();
			return order;
		}

		public async Task<Order> CreateOrder(Order order, CancellationToken cancellation = default)
		{
			order.CreatedAt = DateTime.UtcNow;
			context.Orders.Add(order);
			order.AddEvent(new OrderCreatedEvent(order));
			await context.SaveChangesAsync(cancellation);
			return order;
		}

		public async Task<List<Order>> GetAllOrders(int page, int limit, CancellationToken cancellation = default)
		{
			int pageNumber = page;
			int pageSize = limit;
			int itemsToSkip = (pageNumber - 1) * pageSize;
			var orderList = await context.Orders.Skip(itemsToSkip).Take(pageSize).Include(e => e.Items).ToListAsync(cancellation);

			return orderList;
		}

		public async Task<Order> GetOrderByNumber(int id, CancellationToken cancellation = default)
		{
			Order? order = await context.Orders.Include(e => e.Items)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (order is null) throw new NotFoundException("Order", id);
			return order;
		}

		public async Task<int> GetPagesTotal(int limit, CancellationToken cancellationToken = default)
		{
			var totalRegisters = await context.Orders.CountAsync();
			var totalPages = (int)Math.Ceiling((decimal)totalRegisters / limit);
			return totalPages;
		}

		public async Task<Order> UpdateOrder(Order order, CancellationToken cancellation = default)
		{
			var orderToBeUpdated = await GetOrderByNumber(order.Id, cancellation);
			orderToBeUpdated.CustomerId = order.CustomerId;
			orderToBeUpdated.Branch = order.Branch;


			orderToBeUpdated.AddEvent(new UpdatedOrderEvent(order));
			await context.SaveChangesAsync();
			return orderToBeUpdated;
		}
	}
}
