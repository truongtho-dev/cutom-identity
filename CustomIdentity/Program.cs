using CustomIdentity.Data;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			//CreateHostBuilder(args).Build().Run();
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var loggerFactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					var context = services.GetRequiredService<ApplicationDbContext>();
					var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
					var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
					await ContextSeed.SeedRolesAsync(roleManager);
					await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
				}
				catch (Exception ex)
				{
					var logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex, "An error occurred when seeding the DB");
				}
			}
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
