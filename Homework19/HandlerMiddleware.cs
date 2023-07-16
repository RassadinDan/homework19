using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using System.Threading.Tasks;

namespace Homework19
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class HandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public HandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			await httpContext.Response.WriteAsync("<div> Hello from HandlerMiddleware");
			await _next(httpContext);
			await httpContext.Response.WriteAsync("<div> Bye from HandlerMiddleware");
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class HandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseHandlerMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<HandlerMiddleware>();
		}
	}
}
