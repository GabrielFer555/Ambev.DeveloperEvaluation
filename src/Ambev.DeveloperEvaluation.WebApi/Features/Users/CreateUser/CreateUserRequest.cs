﻿using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username. Must be unique and contain only valid characters.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account.
    /// </summary>
    /// 
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    /// 
    public string Role { get; set; } = string.Empty;
    public AddressDto Address { get; set; } = default!;
    public Name Name { get; set; } = default!;
}