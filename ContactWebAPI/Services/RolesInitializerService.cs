using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactWebAPI.Services
{
	public class RolesInitializerService: IHostedService
	{
		private readonly IServiceProvider _serviceProvider;

		public RolesInitializerService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				string[] roleNames = { "Administrator", "User" };
				foreach (var roleName in roleNames)
				{
					var roleExist = await roleManager.RoleExistsAsync(roleName);
					if (!roleExist)
					{
						await roleManager.CreateAsync(new IdentityRole(roleName));
					}
				}
			}
		}

			public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}
