using ContactWebAPI.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
using ContactWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore;


namespace ContactWebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var init = BuildWebHost(args);
			init.Run();
		}

		public static IWebHost BuildWebHost(string[] args)=>

			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.ConfigureLogging(log => log.AddConsole())
			.Build();
		
	}
}
