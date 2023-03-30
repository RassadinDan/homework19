using Homework19;
using Homework19.Controllers;
using Microsoft.AspNetCore;

class Program
{
	static void Main(string[] args)
	{
		CreateWebHostBuilder(args).Build().Run();
	}

	public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
					.UseStartup<Startup>();
}