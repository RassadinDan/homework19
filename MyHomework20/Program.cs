using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using MyHomework20;
using MyHomework20.Data;

namespace MyHomework20
{

	public class Program
	{ 
		public static void Main(string[] args)
		{
			var init = BuildWebHost(args);

			using (var scope = init.Services.CreateScope())
			{
				var s = scope.ServiceProvider;
				var c = s.GetRequiredService<ContactDBContext>();
				DbInitializer.Initialize(c);
			}

			init.Run();
		}
//	var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//	builder.Services.AddControllersWithViews();

//	var app = builder.Build();

//// Configure the HTTP request pipeline.
//	if (!app.Environment.IsDevelopment())
//	{
//		app.UseExceptionHandler("/Home/Error");
//	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//		app.UseHsts();
//	}


//app.MapControllerRoute(
//name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

	public static IWebHost BuildWebHost(string[] args) =>

		WebHost.CreateDefaultBuilder(args)
		.UseStartup<Startup>()
		.ConfigureLogging(log => log.AddConsole())
		.Build();

	}
}
