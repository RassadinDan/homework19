using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using MyHomework20.Data;
using MyHomework20.DataContext;

namespace MyHomework20
{
    public class Program
	{ 
		public static void Main(string[] args)
		{
			var init = BuildWebHost(args);
			init.Run();
		}

	public static IWebHost BuildWebHost(string[] args) =>

		WebHost.CreateDefaultBuilder(args)
		.UseStartup<Startup>()
		.ConfigureLogging(log => log.AddConsole())
		.Build();
	}
}

