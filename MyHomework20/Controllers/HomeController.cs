using Microsoft.AspNetCore.Mvc;
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

		public IActionResult Index()
		{
			var contacts = new List<Contact>();
			using(ContactDBContext db = new ContactDBContext())
			{
				foreach(Contact contact in db.Contacts)
				{
					contacts.Add(contact);
				}
			}
			return View(contacts);
		}

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
					catch(Exception ex)
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

		[HttpGet]
		public IActionResult Create() 
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateNew(Contact _contact)
		{
			var validator = new ContactValidator();
			var error = validator.Validate(_contact);

			foreach(var _error in error.Errors) { Console.WriteLine($"error:{_error.ErrorCode}, message: {_error.ErrorMessage}, attempted value: {_error.AttemptedValue}"); }

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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Edit(int id)
		{
			using(var db = new ContactDBContext())
			{
				var contact = db.Contacts.Find(id);
				return View(contact);
			}
		}

		[HttpPut]
		public IActionResult EditContact (int id, [FromForm]Contact editedContact)
		{
			Console.Write("test-test");
			var validator = new ContactValidator();
			var error = validator.Validate(editedContact);
			var errors = error.Errors.ToArray();

			foreach (var e in errors)
			{
				Console.WriteLine($"{e.ErrorCode}, {e.ErrorMessage}, attempted value:{e.AttemptedValue}");
			}

			using(var db = new ContactDBContext())
			{
				var cd = db.Contacts.FirstOrDefault(c=> c.Id == id);

				if (cd == null)
				{
					return NotFound();
				}
				cd.Surname = editedContact.Surname;
				cd.Name = editedContact.Name;
				cd.Midname = editedContact.Midname;
				cd.Phone = editedContact.Phone;
				cd.Address = editedContact.Address;
				cd.Description = editedContact.Description;
				db.Contacts.Update(cd);
				db.SaveChanges();
			}
			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			using(var db = new ContactDBContext())
			{
				var cd = db.Contacts.Find(id);
				if (cd != null)
				{
					db.Contacts.Remove(cd);
					db.SaveChanges();
				}
			}
			return RedirectToAction(nameof(Index));
		}

		#region to do
		///Доработать методы изменения и удаления записей, пока что они выдают ошибку  404 или иные.
		///Метод Delete() заработал, но не отработал полностью, по какой-то причине он не вернулся в Index() в конце.
		///Это нужно устранить, и Edit() все еще выдает 404 код.
		#endregion

	}
}