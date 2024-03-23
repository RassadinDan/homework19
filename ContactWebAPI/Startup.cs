using ContactWebAPI.DataContext;
using ContactWebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ContactWebAPI.Services;
using Microsoft.Extensions.Configuration;
using ContactWebAPI.Data;
using ContactWebAPI.Interfaces;

namespace ContactWebAPI
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
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "ContactWebAPI",
					Version = "v1",
					Description = "A simple ASP.NET Core Web API connecting database with client apps" 
				});
			});
			services.AddDbContext<ContactDBContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<Repository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,

						ValidateAudience = false,

						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])),

					};
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireAdministratorRole",
					policy => policy.RequireRole("Administrator"));
				options.AddPolicy("RequireUserRole",
					policy => policy.RequireRole("User"));
			});

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
			});
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactWebAPI V1");
				options.RoutePrefix = "swagger";
			});
			app.UseRouting();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
