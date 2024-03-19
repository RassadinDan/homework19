using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.Eventing.Reader;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IConfiguration _config;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_config = configuration;
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody]UserLogin model)
		{
			if(ModelState.IsValid) 
			{
				var loginResult = await _signInManager.PasswordSignInAsync(model.UserName,
					model.Password,
					false,
					lockoutOnFailure: false);

				if (loginResult.Succeeded) 
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					
					if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
					{
						var token = GenerateJwtToken(user);
						return Ok(new { user.UserName, token });
					}

					return Ok(new { message = "Login sacceeded" });
				}
			}
			
			return BadRequest(new { message = "что-то не так." });
		}

		[HttpPost("loginforweb")]
		public async Task<IActionResult> LoginForWeb([FromBody] UserLogin model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var roles = await _userManager.GetRolesAsync(user);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.NameIdentifier, user.Id),
				};

				foreach (var userRole in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return Ok(new { Token = tokenHandler.WriteToken(token) });
			}

			return Unauthorized();
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]UserRegistration model)
		{
			if (ModelState.IsValid) 
			{
				var user = new User { UserName = model.UserName };
				var createResult = await _userManager.CreateAsync(user, model.Password);

				if(createResult.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return Ok(new { message = "Register succeeded" });
				}
				else
				{
					foreach(var identityError in createResult.Errors) 
					{
						return BadRequest(identityError.Description);
					}
				}
			}
			return View(model);
		}

		/// <summary>
		/// Завершение сессии.
		/// </summary>
		/// <returns></returns>
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new {message = "Logout succeeded"});
		}

		/// <summary>
		/// Генерирация JWT-токена.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		private string GenerateJwtToken(IdentityUser user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
