using Microsoft.AspNetCore.Mvc;

namespace MyHomework20.Component
{
	public class LogoutViewViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
