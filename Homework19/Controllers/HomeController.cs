using Homework19.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework19.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactBook book;

		public HomeController(ApplicationDbContext context)
        {
            _context = context;
            book = new ContactBook(_context);
            book.Fill();
        }

        // GET: HomeController
        public IActionResult Index()
        {
            //Вывод краткой информации обо всех контактах.
			return View(book);
        }

		// GET: HomeController/Details/5
		/// <summary>
		/// Вывод подробной информации о конкретном контакте
		/// </summary>
		/// <param name="id">Индекс контакта в коллекции контактов ContactBook</param>
		/// <returns></returns>
		public IActionResult Details(int id)
        {
            if(id <= -1 && id > book.Contacts.Count)
            {
                return NotFound();
            }

            return View(book.Contacts[id]);
        }

        // GET: HomeController/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Новый контакт";
            return View();
        }

        // POST: HomeController/CreateNew
        // не проходит этот метод, Create работает, а на этот уже не переходит, видимо не обработано нажатие на submit
        // проходит переадрессация к этому методу но сам метод не дорабатывает 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNew(string surname, string name, string midname,
            int phone, string address, string description)
        {
            using (var db = new ApplicationDbContext())
            {
                var contact = book.factory.CreateContact(surname, name, midname, phone, address, description);
                db.Contacts.Add(contact);
                db.SaveChanges();
            }

            return Redirect("Index");
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if(id < 0)
            {
                return Redirect(Index);
            }
            else
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
