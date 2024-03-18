using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyHomework20.Data;
using MyHomework20.DataContext;
using MyHomework20.Interfaces;
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

			services.AddTransient<IContactData, ContactDataApi>();
			
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

			services.AddHttpClient<AuthService>("UnsafeSSLClient", client =>
			{

			}).ConfigurePrimaryHttpMessageHandler(() =>
			{
				return new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicy) => true
				};
			});

			services.AddSession();
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, RoleManager<IdentityRole> roleManager)
		{
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			InitializeRoles(roleManager).Wait();
		}

		private async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
		{
			if(!await roleManager.RoleExistsAsync("Administrator"))
			{
				await roleManager.CreateAsync(new IdentityRole("Administrator"));
			}
			if(!await roleManager.RoleExistsAsync("User"))
			{
				await roleManager.CreateAsync(new IdentityRole("User"));
			}
		}
	}
}
