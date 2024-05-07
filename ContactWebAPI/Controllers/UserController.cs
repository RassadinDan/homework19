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
