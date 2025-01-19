

namespace Ambev.DeveloperEvaluation.ORM
{
	public static class Extensions
	{
		public static WebApplication UseAutoMigration(this WebApplication webApplication)
		{
			using var scope = webApplication.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

			context.Database.MigrateAsync().GetAwaiter().GetResult();
			return webApplication;
		}
	}
}
