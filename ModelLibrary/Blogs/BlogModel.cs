using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Blogs
{
	public class BlogModel
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Preview { get; set; }
		public string Description { get; set; }

		public IFormFile? ImageFile { get; set; }

		public string? ImageDataUrl { get; set; }
	}
}
