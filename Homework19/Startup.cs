using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Homework19
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options => options.EnableEndpointRouting = false);

			// Контекст понадобится для извлечения контактов из базы данных.
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(@"DataSource = (localdb)\MSSQLLocalDB;InitialCatalog = ContactData;"));
		}
		public void Configure(IApplicationBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

			app.UseExceptionHandler(options => { options.UseHandlerMiddleware(); });
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
