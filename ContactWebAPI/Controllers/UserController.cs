using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;

namespace ContactWebAPI.Controllers
{
    public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new UserLogin()
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLogin model)
		{
			if(ModelState.IsValid) 
			{
				var loginResult = await _signInManager.PasswordSignInAsync(model.UserName,
					model.Password,
					false,
					lockoutOnFailure: false);

				if (loginResult.Succeeded) 
				{
					if(Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}

					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError("", "Пользователь не найден");
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new UserRegistration());
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserRegistration model)
		{
			if (ModelState.IsValid) 
			{
				var user = new User { UserName = model.UserName };
				var createResult = await _userManager.CreateAsync(user, model.Password);

				if(createResult.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach(var identityError in createResult.Errors) 
					{
						ModelState.AddModelError("", identityError.Description);
					}
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
