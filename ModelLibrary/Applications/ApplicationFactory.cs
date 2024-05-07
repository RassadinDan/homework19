using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Applications
{
	public class ApplicationFactory
	{
		public Application Create(string name, string text, string email)
		{
			var time = DateTime.Now;
			var application = new Application()
			{
				DateTime = time,
				Name = name,
				Text = text,
				Email = email,
				Status = ApplicationStatus.Accepted
			};
			return application;
		}
	}
}
