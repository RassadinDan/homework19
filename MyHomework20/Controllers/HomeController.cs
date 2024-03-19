using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using MyHomework20.DataContext;
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

		public IActionResult Index()
		{
			var data = new ContactDataApi();
			var contacts = data.GetContacts();
			
			return View(contacts);
		}

		/// <summary>
		/// Вывод подробной информации о выбранном контакте
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		public IActionResult Details(int id)
		{
			if (id >= 0)
			{
				var db = new ContactDataApi();
				try
				{
					var contact = db.GetContactById(id);
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

		/// <summary>
		/// Загрузка формы для заполнения данных нового контакта.
		/// </summary>
		/// <returns></returns>
		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Отправка данных нового контакта на сервер.
		/// </summary>
		/// <param name="_contact"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult CreateNew(Contact _contact)
		{
			var validator = new ContactValidator();
			var error = validator.Validate(_contact);

			foreach (var _error in error.Errors) { Console.WriteLine($"error:{_error.ErrorCode}, message: {_error.ErrorMessage}, attempted value: {_error.AttemptedValue}"); }

			var api = new ContactDataApi();
			try
			{
				api.AddContact(_contact);
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

		public IActionResult Edit(int id)
		{
			var api = new ContactDataApi();
			var contact = api.GetContactById(id);
			if(contact == null)
			{
				return NotFound();
			}
			return View(contact);
		}

		/// <summary>
		/// Метод отправки обновленных данных контакта на сервер.
		/// </summary>
		/// <param name="id">идентификатор контакта</param>
		/// <param name="editedContact">полученные изменения</param>
		/// <returns></returns>
		[HttpPost("Home/EditContact/{id}")]

		public IActionResult EditContact (int id, [FromForm]Contact editedContact)
		{
			Console.Write($"--->{editedContact.Id}, {editedContact.Surname}, {editedContact.Name}, {editedContact.Midname}," +
				$"{editedContact.Phone}, {editedContact.Address}, {editedContact.Description}.\n");

			var api = new ContactDataApi();

			api.Update(editedContact);

			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Удаление данных контакта.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		public IActionResult Delete(int id)
		{

			var api = new ContactDataApi();
			api.Remove(id);

			return RedirectToAction(nameof(Index));
		}
	}
}