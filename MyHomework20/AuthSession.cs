using MyHomework20.Models;
using ModelLibrary.Auth;

namespace MyHomework20
{
	public static class AuthSession
	{
		public static bool IsAuthenticated { get; set; }

		public static User? User { get; set; }

		public static IList<string>? Roles { get; set; }

		public static string? Token {  get; set; }

		public static void ClearSession() 
		{ 
			User = null;
			IsAuthenticated = false;
			Roles = null;
			Token = string.Empty;
		}
	}
}
