using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.Eventing.Reader;
using ContactWebAPI.Interfaces;
using ModelLibrary.Auth.Dto;
using System.Reflection.Metadata.Ecma335;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<UserController> _logger;

		public UserController(IUserRepository userRepository, ILogger<UserController> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody]LoginRequest model)
		{
			if(ModelState.IsValid)
			{
				var loginResult = await _userRepository.Login(model);
				
				if (loginResult.User != null && loginResult.Token != string.Empty) 
				{
					_logger.Log(LogLevel.Information, loginResult.User.UserName);
					return Ok(loginResult);
				}
			}		
			return BadRequest(new { message = "что-то не так." });
		}

		//[HttpPost("loginforweb")]
		//public async Task<IActionResult> LoginForWeb([FromBody] UserLogin model)
		//{
		//	var user = await _userManager.FindByNameAsync(model.UserName);
		//	if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
		//	{
		//		var roles = await _userManager.GetRolesAsync(user);

		//		var claims = new List<Claim>
		//		{
		//			new Claim(ClaimTypes.Name, user.UserName),
		//		};

		//		foreach (var userRole in roles)
		//		{
		//			claims.Add(new Claim(ClaimTypes.Role, userRole));
		//			_logger.Log(LogLevel.Information, userRole);
		//		}

		//		var tokenHandler = new JwtSecurityTokenHandler();
		//		var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
		//		var tokenDescriptor = new SecurityTokenDescriptor
		//		{
		//			Subject = new ClaimsIdentity(claims),
		//			Expires = DateTime.UtcNow.AddDays(7),
		//			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		//		};
		//		var token = tokenHandler.CreateToken(tokenDescriptor);
		//		return Ok(new { Token = tokenHandler.WriteToken(token) });
		//	}

		//	return Unauthorized();
		//}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]RegistrationRequest model)
		{
			if (ModelState.IsValid) 
			{
				var user = await _userRepository.Register(model);
				if(user != null)
				{
					return Ok(new { message = "Register succeeded" });
				}
			}
			return BadRequest();
		}

		/// <summary>
		/// Завершение сессии.
		/// </summary>
		/// <returns></returns>
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			Response.Headers.Remove("Authorization");
			return Ok(new {message = "Logout succeeded"});
		}
	}
}
