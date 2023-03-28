using Homework19;
using Microsoft.AspNetCore;

class Program
{
	static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddRazorPages();
		builder.Services.AddControllers();
		var app = builder.Build();

		//
		app.MapControllerRoute(name: "default", 
							pattern: "{controller = Home}/{action = Index}");

		app.Run();
	}

	public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
					.UseStartup<Startup>();
}