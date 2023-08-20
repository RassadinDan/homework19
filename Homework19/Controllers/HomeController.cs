using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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

        private IValidator<Contact> _validator;

        public ContactBook book;

		public HomeController(ApplicationDbContext context, IValidator<Contact> validator)
        {
            _context = context;
            _validator = validator;
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
            return View(book);
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {

			ValidationResult result= await _validator.ValidateAsync(contact);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return Redirect(nameof(Create));
            }

            _context.SaveChanges();
            TempData["notice"] = "Contact created successfully";

            return Redirect(nameof(Index)); 
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
        public ActionResult Edit(int id, IFormCollection collection,
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
