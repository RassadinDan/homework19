using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using MyHomework20.Models;
using System.Diagnostics;

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
			var contacts = new List<Contact>();
			using (ContactDBContext db = new ContactDBContext())
			{
				foreach (Contact contact in db.Contacts)
				{
					contacts.Add(contact);
				}
			}
			return View(contacts);
		}

		/// <summary>
		/// Вывод подробной информации о выбранном контакте
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Authorize]
		public IActionResult Details(int id)
		{
			if (id > 0)
			{
				using (var db = new ContactDBContext())
				{
					try
					{
						var contact = db.Contacts.Find(id);
						return View(contact);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						return RedirectToAction(nameof(Error));
					}
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
		[Authorize]
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
		[Authorize]
		public IActionResult CreateNew(Contact _contact)
		{
			var validator = new ContactValidator();
			var error = validator.Validate(_contact);

			foreach (var _error in error.Errors) { Console.WriteLine($"error:{_error.ErrorCode}, message: {_error.ErrorMessage}, attempted value: {_error.AttemptedValue}"); }

			using (ContactDBContext db = new ContactDBContext())
			{
				try
				{
					db.Add(_contact);
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return RedirectToAction("Error");
				}
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
			using (var db = new ContactDBContext())
			{
				var contact = db.Contacts.Find(id);
				if(contact == null)
				{
					return NotFound();
				}
				return View(contact);
			}
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

			using(var db = new ContactDBContext()) 
			{
				var existingContact = db.Contacts.FirstOrDefault(c=> c.Id == id);

				if (existingContact == null)
				{
					return NotFound();
				}

				existingContact.Surname = editedContact.Surname;
				existingContact.Name = editedContact.Name;
				existingContact.Midname = editedContact.Midname;
				existingContact.Phone = editedContact.Phone;
				existingContact.Address = editedContact.Address;
				existingContact.Description = editedContact.Description;

				db.SaveChanges();
			}
			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Удаление данных контакта.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		//[HttpDelete]
		public IActionResult Delete(int id)
		{
			using(var db = new ContactDBContext())
			{
				var existingContact = db.Contacts.FirstOrDefault(c => c.Id == id);

				if (existingContact != null)
				{
					db.Contacts.Remove(existingContact);
					db.SaveChanges();
				}
			}
			return RedirectToAction(nameof(Index));
		}
	}
}