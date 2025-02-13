using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ambev.DeveloperEvaluation.Integration.Auth
{
	public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public AuthHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder
		) : base(options, logger, encoder) { }

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, "test-user"),
				new Claim(ClaimTypes.Name, "Test User"),
				new Claim(ClaimTypes.Role, "Admin") // Adiciona um papel para passar pela autorização
			};

			var identity = new ClaimsIdentity(claims, "TestingPurposes");
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, "TestScheme");

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
