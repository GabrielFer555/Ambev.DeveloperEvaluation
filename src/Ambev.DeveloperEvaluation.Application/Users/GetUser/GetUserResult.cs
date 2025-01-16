using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetUserResult
{
	/// <summary>
	/// The unique identifier of the user
	/// </summary>
	public Guid Id { get; set; } = default!;
	public string Username { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public string Phone { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public UserStatus Status { get; set; }

	public UserRole Role { get; set; }

	public Name Name { get; set; } = default!;
	public AddressDto Address { get; set; } = default!;
}
