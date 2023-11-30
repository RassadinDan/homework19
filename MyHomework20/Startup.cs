using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyHomework20.DataContext;
using MyHomework20.Models;

namespace MyHomework20
{
    public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			});

			services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ContactDBContext>()
                .AddDefaultTokenProviders();

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
				options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
				options.LoginPath = "/User/Login";
				options.LogoutPath = "/User/Logout";
				options.SlidingExpiration = true;
			});

			services.AddDbContext<ContactDBContext>(options => options.UseSqlServer(
				Configuration.GetConnectionString("DefaultConnection")));
			//services.AddTransient
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

		}
	}
}
