using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
	public class CartRepository(DefaultContext _context) : ICartRepository
	{
		public async Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken = default)
		{
			cart.Date = DateTime.UtcNow;
			_context.Carts.Add(cart);
			await _context.SaveChangesAsync(cancellationToken);
			return cart;

		}

		public async Task<bool> DeleteCart(int id, CancellationToken cancellationToken = default)
		{
			var cart = await GetCartById(id);
			_context.CartItems.RemoveRange(cart.Products);
			_context.Carts.Remove(cart);
			await _context.SaveChangesAsync(cancellationToken);
			return true;

		}

		public async Task<List<Cart>> GetAllCarts(int page, int limit, CancellationToken cancellationToken = default)
		{
			int pageNumber = page;
			int pageSize = limit;
			int itemsToSkip = (pageNumber - 1) * pageSize;
			var cartsList = await _context.Carts.Skip(itemsToSkip).Take(pageSize).Include(e => e.Products).ToListAsync(cancellationToken);

			return cartsList;
		}

		public async Task<Cart> GetCartById(int id, CancellationToken cancellationToken = default)
		{
			var cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);
			if(cart is null)
			{
				throw new NotFoundException("Cart", id);
			}
			return cart;
		}

		public async Task<Cart> UpdateCartAsync(Cart cart, CancellationToken cancellationToken = default)
		{
			var cartToBeUpdated = await GetCartById(cart.Id);
			cartToBeUpdated.Products = cart.Products;
			cartToBeUpdated.UserId = cart.UserId;
			cartToBeUpdated.Date = DateTime.UtcNow;
			await _context.SaveChangesAsync(cancellationToken);
			return cartToBeUpdated;
		}
		public async Task<int> GetTotalPages(int pageSize)
		{
			var totalRegisters = await _context.Carts.CountAsync();
			var totalPages = (int)Math.Ceiling((decimal)totalRegisters / pageSize);
			return totalPages;
		}

	}
}
