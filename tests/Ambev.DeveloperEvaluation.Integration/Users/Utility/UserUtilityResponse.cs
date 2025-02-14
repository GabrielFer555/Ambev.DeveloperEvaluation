namespace Ambev.DeveloperEvaluation.Integration.Users.Utility
{
    public record UserUtilityResponse
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty ;

        public string Role { get; set; } = string.Empty;

        public Name Name { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;
    }
}
