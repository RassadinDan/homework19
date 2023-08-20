using FluentValidation;
using Homework19.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Homework19
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options => options.EnableEndpointRouting = false);

			// Контекст понадобится для извлечения контактов из базы данных.
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(@"DataSource = (localdb)\MSSQLLocalDB;InitialCatalog = ContactData;"));

			services.AddScoped<IValidator<Contact>, ContactValidator>();
		}
		public void Configure(IApplicationBuilder app)
		{
			// Настройка конвеера обработки запросов, подключения необходимого ПО промежуточного слоя.

			app.UseExceptionHandler(options => 
			{
				options.Run(
					async context =>
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						context.Response.ContentType = "text/html";
						var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
						if(exceptionObject != null) 
						{
							var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";
							await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
						}
					});
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();


			app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}"); 
			});

			app.UseMvc(
				r =>
				{
					r.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
				});
		}
	}
}
