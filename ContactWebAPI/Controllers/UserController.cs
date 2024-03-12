using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

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


		[HttpPost("login"),/* ValidateAntiForgeryToken*/]
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
			//ModelState.AddModelError("", "Пользователь не найден");
			//return View(model);
		}

		//[HttpGet]
		//public IActionResult Register()
		//{
		//	return View(new UserRegistration());
		//}

		[HttpPost("register"), /*ValidateAntiForgeryToken*/]
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
					//return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach(var identityError in createResult.Errors) 
					{
						//ModelState.AddModelError("", identityError.Description);
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
		[HttpPost("logout"), /*ValidateAntiForgeryToken*/]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new {message = "Logout succeeded"});
			//return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Генерирация JWT-токена.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		private string GenerateJwtToken(IdentityUser user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
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
