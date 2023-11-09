using System.ComponentModel.DataAnnotations;

namespace MyHomework20
{
	public class UserLogin
	{
		[Required, MaxLength(30)]
		public string UserName { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
