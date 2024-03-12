using ContactWebAPI.DataContext;
using ContactWebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
			//services.AddMvc();
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
			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<ContactDBContext>()
				.AddDefaultTokenProviders();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "https://localhost",
						ValidAudience = "client_app",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TheMostSecretKeyInTheWorldAndWholeUniverse"))
					};
				});
		}
		public void Configure(IApplicationBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactWebAPI V1");
				options.RoutePrefix = "swagger";
			});
			app.UseRouting();
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
