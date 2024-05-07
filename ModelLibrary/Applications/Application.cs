using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Applications
{
	public class Application
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public string Email { get; set; }
		public ApplicationStatus Status { get; set; }
	}

	public enum ApplicationStatus
	{
		Accepted,
		InProgress,
		Completed,
		Rejected,
		Cancelled
	}
}
