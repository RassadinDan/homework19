using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Projects
{
	public class Project
	{
		public int Id { get; set; }
		public string Preview {  get; set; }
		public string Description { get; set; }

		public byte[] ImageData { get; set; }
		public string ImageContentType { get; set; }
	}
}
