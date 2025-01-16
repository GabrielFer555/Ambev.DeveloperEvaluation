using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class UserRepository : IUserRepository
{
	private readonly DefaultContext _context;

	public UserRepository(DefaultContext context)
	{
		_context = context;
	}

	public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
	{
		await _context.Users.AddAsync(user, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return user;
	}

	public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var user = await GetByIdAsync(id, cancellationToken);
		if (user == null)
			return false;

		user.Deactivate();
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<List<User>> GetAllAsync(int? page, int? limit, CancellationToken cancellationToken = default)
	{
		int pageSelected = page ?? 1;
		int limitSelected = limit ?? 10;
		int itemsToSkip = (pageSelected - 1) * limitSelected;
		var users = await _context.Users.Skip(itemsToSkip).Take(limitSelected).ToListAsync(cancellationToken);
		return users;
	}
	public async Task<int> GetTotalPages(int pageSize)
	{
		var totalRegisters = await _context.Users.CountAsync();
		var totalPages = (int) Math.Ceiling((decimal) totalRegisters / pageSize);
		return totalPages;
	}

	public async Task<User> UpdateUser(User user, CancellationToken cancellationToken = default)
	{
		var userToUpdate = await _context.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
		if(userToUpdate is null)
		{
			throw new UserNotFoundException(user.Id);
		}

		userToUpdate.UpdatedAt = DateTime.UtcNow;
		userToUpdate.Email = user.Email;
		userToUpdate.Address = user.Address;
		userToUpdate.Password = user.Password;
		userToUpdate.Name = user.Name;
		userToUpdate.Role = user.Role;
		userToUpdate.Status	= user.Status;
		await _context.SaveChangesAsync();
		return userToUpdate;
		
	}
}


