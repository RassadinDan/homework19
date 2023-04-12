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
        public ActionResult Index()
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
		public ActionResult Details(int id)
        {
            if(id <= -1 && id > book.Contacts.Count)
            {
                return NotFound();
            }

            return View(book.Contacts[id]);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
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
