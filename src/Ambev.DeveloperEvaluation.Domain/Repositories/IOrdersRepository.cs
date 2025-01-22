using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
	public interface IOrdersRepository
	{
		Task<Order> GetOrderByNumber(int id, CancellationToken cancellation = default);
		Task<Order> CreateOrder(Order order, CancellationToken cancellation = default);
		Task<Order> UpdateOrder(Order order, CancellationToken cancellation = default);
		Task<bool> CancelOrder(int id, CancellationToken cancelToken = default);
		Task<List<Order>> GetAllOrders(int page, int limit, CancellationToken cancellation = default);
		Task<int> GetPagesTotal(int limit, CancellationToken cancellationToken = default);
		Task<Order> CancelOrderItem(int orderId, int orderItemId, CancellationToken cancelToken = default);

	}
}
