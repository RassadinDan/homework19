﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Homework19
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class MyMiddleware
	{
		private readonly RequestDelegate _next;

		public MyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{

			return _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class MyMiddlewareExtensions
	{
		public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<MyMiddleware>();
		}
	}
}
