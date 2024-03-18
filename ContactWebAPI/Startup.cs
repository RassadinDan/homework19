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
				options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ContactData_1;Integrated Security = true");
			});
			services.AddIdentityCore<User>()
				.AddEntityFrameworkStores<ContactDBContext>()
				.AddDefaultTokenProviders();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "https://localhost:7062/",
						ValidAudience = "client_app",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TheMostSecretKeyInTheWorldAndWholeUniverse"))
					};
				});
			services.AddHostedService<RolesInitializerService>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactWebAPI V1");
				options.RoutePrefix = "swagger";
			});
			app.UseRouting();
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
