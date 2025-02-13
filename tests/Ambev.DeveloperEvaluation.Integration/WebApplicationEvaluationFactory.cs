using Ambev.DeveloperEvaluation.Integration.Auth;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ambev.DeveloperEvaluation.Integration
{
	public class WebApplicationEvaluationFactory:WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services => {
				services.RemoveAll(typeof(DbContextOptions<DefaultContext>));

				var connString = GetConnectionString();

				services.AddAuthentication("TestScheme")
			   .AddScheme<AuthenticationSchemeOptions, AuthHandler>("TestingPurposes", options => { });
			

			services.AddNpgsql<DefaultContext>(connString);
				var dbContext = CreateDbContext(services);
				dbContext.Database.EnsureDeleted();
			});
				
		}
		private static string? GetConnectionString()
		{
			var configurationBuilder = new ConfigurationBuilder()
				.AddUserSecrets<WebApplicationEvaluationFactory>()
				.Build();


			var str = configurationBuilder.GetConnectionString("DefaultConnection");

			return str;
		}

		private static DefaultContext CreateDbContext(IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();
			var scope = provider.CreateScope();	
			var dbContext = scope.ServiceProvider.GetService<DefaultContext>();

			return dbContext!;
		}
	}
}
