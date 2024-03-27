namespace ContactDesktop.Auth.Dto
{
	public class RegistrationRequest
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Role { get; set; }
	}
}
