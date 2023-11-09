﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MyHomework20
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddIdentity<User, IdentityRole>();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequiredLength = 6;

				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
				options.Lockout.AllowedForNewUsers = true;
			});
		}
		public void Configure(IApplicationBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		}
	}
}
