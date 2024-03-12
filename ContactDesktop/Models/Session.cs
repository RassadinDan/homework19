using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDesktop.Models
{
	public static class Session
	{
		public static string UserName { get; set; }

		public static string AuthToken { get; set; }

		public static void ClearSession()
		{
			UserName = null;
			AuthToken = null;
		}
	}
}
