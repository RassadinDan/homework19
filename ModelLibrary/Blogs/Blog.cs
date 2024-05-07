
namespace ModelLibrary.Blogs
{
	public class Blog
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Preview { get; set; }
		public string Description { get; set; }

		public byte[] ImageData { get; set; }
		public string ImageContentType { get; set; }

	}
}
