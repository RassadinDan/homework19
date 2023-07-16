using Homework19.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics;
using System.Numerics;

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
        [HttpGet]
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
        [HttpGet]
		public IActionResult Details(int id)
        {
            if(id <= -1 && id > book.Contacts.Count)
            {
                return NotFound();
            }

            return View(book.Contacts[id]);
        }

        // GET: HomeController/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Новый контакт";
            return View();
        }

        // POST: HomeController/CreateNew
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm(Name = "surname")] string surname, [FromForm(Name = "name")] string name,
            [FromForm(Name = "midname")] string midname, [FromForm(Name ="phone)")] int phone, [FromForm(Name = "address")] string address,
            [FromForm(Name = "description")] string description)
        {
            if (ModelState.IsValid)
            {
				var contact = book.factory.CreateContact(surname, name, midname, phone, address, description);

                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            else{ return BadRequest(); }
        }

        // GET: HomeController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if(id < 0)
            {
                return BadRequest();
            }
            else { return View(book.Contacts[id]); }
		}

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(int id, IFormCollection collection,
            string surname, string name, string midname, int phone, string address, string description)
        {
            if(id < 0 || !ModelState.IsValid)
            {
				return BadRequest(); ;
            }
            else 
            {
                var contact = book.Contacts[id];
                contact.Surname= surname;
                contact.Name= name;
                contact.Midname= midname;
                contact.Phone= phone;
                contact.Address = address;
                contact.Description= description;
                _context.SaveChanges();
                return Redirect(nameof(Index));
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
