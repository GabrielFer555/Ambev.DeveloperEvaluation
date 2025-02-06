namespace Ambev.DeveloperEvaluation.Domain.Enums;

public enum UserRole
{
    [Description("None")]
    None = 0,
	[Description("Customer")]
	Customer,
	[Description("Manager")]
	Manager,
	[Description("Admin")]
	Admin,
}