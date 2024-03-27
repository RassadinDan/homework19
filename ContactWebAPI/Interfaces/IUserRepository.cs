using ModelLibrary.Auth.Dto;
using ModelLibrary.Auth;

namespace ContactWebAPI.Interfaces
{
	public interface IUserRepository
	{
		bool IsUniqueuser(string username);
		Task<LoginResponse> Login(LoginRequest model);
		Task<User> Register(RegistrationRequest model);
	}
}
