using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		//[HttpGet]
		//public IActionResult Login(string returnUrl)
		//{
		//	return View(new UserLogin()
		//	{
		//		ReturnUrl = returnUrl
		//	});
		//}

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
					
					if (user != null)
					{
						return Ok(new { UserName = user.UserName });
					}
					//if(Url.IsLocalUrl(model.ReturnUrl))
					//{
					//	return Redirect(model.ReturnUrl);
					//}

					return Ok(new { message = "Login sacceeded" });
						//RedirectToAction("Index", "Home");
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

		[HttpPost("logout"), /*ValidateAntiForgeryToken*/]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new {message = "Logout succeeded"});
			//return RedirectToAction("Index", "Home");
		}
	}
}
