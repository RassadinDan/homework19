using Microsoft.AspNetCore.Builder;
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

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromMinutes(10);
				options.LoginPath = "/User/Login";
				options.LogoutPath = "/User/Logout";
				options.SlidingExpiration = true;
			});
		}
		public void Configure(IApplicationBuilder app)
		{
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

		}
	}
}
