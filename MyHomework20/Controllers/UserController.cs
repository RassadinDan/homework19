using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Login(/*string returnUrl*/)
		{
			var returnUrl = "https://localhost:7062/Home/";	
			return View(new UserLogin()
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost, /*ValidateAntiForgeryToken*/]
		public async Task<IActionResult> Login(UserLogin model)
		{
			var token = await _authService.LoginAsync(model);
			if (token != null)
			{
				AuthSession.IsAuthenticated = true;
				AuthSession.User = new User { UserName = model.UserName };
				AuthSession.Token = token;
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
			return View(new UserRegistration());
		}

		[HttpPost, /*ValidateAntiForgeryToken*/]
		public async Task<IActionResult> Register(UserRegistration model)
		{
			if (ModelState.IsValid) 
			{
				var createResult = await _authService.RegisterAsync(model);

				if(createResult == true)
				{
					var login = new UserLogin() { UserName = model.UserName, Password = model.Password };
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

		[HttpPost, /*ValidateAntiForgeryToken*/]
		public async Task<IActionResult> Logout()
		{
			await _authService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
