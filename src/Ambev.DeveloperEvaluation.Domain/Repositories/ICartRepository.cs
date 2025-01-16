using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
	public interface ICartRepository
	{
		Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken = default!);
		Task<Cart> UpdateCartAsync(Cart cart, CancellationToken cancellationToken = default!);
		Task<Cart> GetCartById(int id, CancellationToken cancellationToken = default!);
		Task<List<Cart>> GetAllCarts(int page, int limit, CancellationToken cancellationToken = default!);
		Task<bool> DeleteCart(int id, CancellationToken cancellationToken = default!);
		Task<int> GetTotalPages(int pageSize);
	}
}
