using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Auth.Dto;
using MyHomework20.Models;

namespace MyHomework20.Controllers
{
    public class UserController : Controller
	{
		private readonly AuthService _authService;

		public UserController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginRequest());
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginRequest model)
		{
			var response = await _authService.LoginAsync(model);
			if (response != null)
			{
				AuthSession.IsAuthenticated = true;
				AuthSession.User = response.User;
				AuthSession.Token = response.Token;
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.ErrorMessage = "Неверное имя пользователя или пароль";
				return View();
			}
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegistrationRequest());
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegistrationRequest model)
		{
			if (ModelState.IsValid) 
			{
				var createResult = await _authService.RegisterAsync(model);

				if(createResult == true)
				{
					var login = new LoginRequest() { UserName = model.UserName, Password = model.Password };
					await _authService.LoginAsync(login);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction("Error", "Home");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _authService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
