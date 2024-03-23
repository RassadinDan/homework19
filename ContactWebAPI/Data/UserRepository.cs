using ContactWebAPI.DataContext;
using ContactWebAPI.Interfaces;
using ContactWebAPI.Models;
using ContactWebAPI.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactWebAPI.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly ContactDBContext _db;
		private string secretKey;
		public UserRepository(ContactDBContext db, IConfiguration config)
		{
			_db = db;
			secretKey = config.GetValue<string>("JwtSettings:SecretKey");
		}
		public bool IsUniqueuser(string username)
		{
			var user = _db.Users.FirstOrDefault(x => x.UserName == username);
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponse> Login(LoginRequest model)
		{
			var user = _db.Users.FirstOrDefault(u=> u.UserName.ToLower()==model.UserName.ToLower() && u.Password == model.Password);
			if (user == null)
			{
				return new LoginResponse()
				{
					Token = "",
					User = null
				};
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var loginResponse = new LoginResponse()
			{
				Token = tokenHandler.WriteToken(token),
				User = user
			};
			return loginResponse;
		}

		public async Task<User> Register(RegistrationRequest model)
		{
			User user = new()
			{
				UserName = model.UserName,
				Password = model.Password,
				Role = model.Role
			};
			_db.Users.Add(user);
			await _db.SaveChangesAsync();
			user.Password = "";
			return user;
		}
	}
}
