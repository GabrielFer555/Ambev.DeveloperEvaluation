﻿using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateUserResult
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
