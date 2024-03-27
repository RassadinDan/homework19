using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHomework20.Models;
using System.Diagnostics;
using MyHomework20.Data;

namespace MyHomework20.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Основная страница с выводом контактов.
		/// </summary>
		/// <returns></returns>

		public async Task<IActionResult> Index()
		{
			var data = new ContactDataApi();
			var contacts = await data.GetContacts();
			
			return View(contacts);
		}

		/// <summary>
		/// Вывод подробной информации о выбранном контакте
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		public async Task<IActionResult> Details(int id)
		{
			if (AuthSession.User != null)
			{
				if (id >= 0)
				{
					var dataApi = new ContactDataApi();
					try
					{
						var contact = await dataApi.GetContactById(id);
						return View(contact);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						return RedirectToAction(nameof(Error));
					}
				}

				else
				{
					return RedirectToAction(nameof(Error));
				}
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}

		/// <summary>
		/// Загрузка формы для заполнения данных нового контакта.
		/// </summary>
		/// <returns></returns>
		[HttpGet]

		public IActionResult Create()
		{
			if (AuthSession.User == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				return View();
			}
		}

		/// <summary>
		/// Отправка данных нового контакта на сервер.
		/// </summary>
		/// <param name="_contact"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> CreateNew(Contact _contact)
		{
			var validator = new ContactValidator();
			var error = validator.Validate(_contact);

			foreach (var _error in error.Errors) { Console.WriteLine($"error:{_error.ErrorCode}, message: {_error.ErrorMessage}, attempted value: {_error.AttemptedValue}"); }

			var api = new ContactDataApi();
			try
			{
				await api.AddContact(_contact);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return RedirectToAction("Error");
			}
			
			return RedirectToAction("Index");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		/// <summary>
		/// Универсальная страница ошибки.
		/// </summary>
		/// <returns></returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>
		/// Форма для обновления данных контакта.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		public async Task<IActionResult> Edit(int id)
		{
			if (AuthSession.User != null && AuthSession.User.Role == "admin")
			{
				var api = new ContactDataApi();
				var contact = await api.GetContactById(id);
				if (contact == null)
				{
					return NotFound();
				}
				return View(contact);
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}

		/// <summary>
		/// Метод отправки обновленных данных контакта на сервер.
		/// </summary>
		/// <param name="id">идентификатор контакта</param>
		/// <param name="editedContact">полученные изменения</param>
		/// <returns></returns>
		[HttpPost("Home/EditContact/{id}")]

		public async Task<IActionResult> EditContact (int id, [FromForm]Contact editedContact)
		{
			if (AuthSession.User != null && AuthSession.User.Role == "admin")
			{
				Console.Write($"--->{editedContact.Id}, {editedContact.Surname}, {editedContact.Name}, {editedContact.Midname}," +
					$"{editedContact.Phone}, {editedContact.Address}, {editedContact.Description}.\n");

				var api = new ContactDataApi();

				await api.Update(editedContact);

				return RedirectToAction(nameof(Index));
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}

		/// <summary>
		/// Удаление данных контакта.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		public async Task<IActionResult> Delete(int id)
		{
			if (AuthSession.User != null && AuthSession.User.Role == "admin")
			{
				var api = new ContactDataApi();
				await api.Remove(id);

				return RedirectToAction(nameof(Index));
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}
	}
}