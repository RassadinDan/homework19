﻿using Microsoft.AspNetCore.Builder;
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

			app.UseStaticFiles();
			app.UseMiddleware<MyMiddleware>();
			app.UseRouting();
			app.UseAuthorization();

			app.UseMvc(
				r=>
				{
					r.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
				});

			//app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(
			//	name: "default",
			//	pattern: "{controller=Home}/{action=Index}/{id?}"); });
		}
	}
}
