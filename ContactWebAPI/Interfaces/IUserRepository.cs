using ContactWebAPI.Models;
using ContactWebAPI.Models.Dto;

namespace ContactWebAPI.Interfaces
{
	public interface IUserRepository
	{
		bool IsUniqueuser(string username);
		Task<LoginResponse> Login(LoginRequest model);
		Task<User> Register(RegistrationRequest model);
	}
}
