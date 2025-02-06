namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
	public class UpdateUserResult
	{
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
}
