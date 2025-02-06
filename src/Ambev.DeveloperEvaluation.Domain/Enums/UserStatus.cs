

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public enum UserStatus
{
	[Description("Unknown")]
	Unknown = 0,
	[Description("Active")]
	Active,
	[Description("Inactive")]
	Inactive,
	[Description("Suspended")]
	Suspended
}
