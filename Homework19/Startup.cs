using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Homework19
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options => options.EnableEndpointRouting = false);
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
						template: "{controller=Home}/{action=Index}");
				});
		}
	}
}
