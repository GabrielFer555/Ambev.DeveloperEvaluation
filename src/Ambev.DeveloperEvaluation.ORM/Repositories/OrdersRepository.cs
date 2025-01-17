using System.Threading;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

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
			await context.SaveChangesAsync();
			return true;
		}

		public async Task<Order> CreateOrder(Order order, CancellationToken cancellation = default)
		{
			order.CreatedAt = DateTime.UtcNow;
			context.Orders.Add(order);
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
			orderToBeUpdated.Items = order.Items;
			return orderToBeUpdated;
		}
	}
}
