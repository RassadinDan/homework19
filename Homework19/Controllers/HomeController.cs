using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Homework19.Models;
using Homework19.Views.Home;
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
        //private readonly ApplicationDbContext _context;

        //private IValidator<Contact> _validator;

        public ContactBook book;

		public HomeController()
        {
            book = new ContactBook();
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
                return Redirect("~/Error");
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

		// POST: HomeController/Create
		[HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreatePost")]
        public IActionResult CreateNew([Bind("Surname, Name, Midname, Phone, Address, Description")] Contact contact)
        {
            var validator = new ContactValidator();
			ValidationResult result= validator.Validate(contact);

            if (!result.IsValid)
            {
                var errors = result.Errors.ToArray();
                foreach(var error in errors)
                {
                    Console.WriteLine($"{error.ErrorMessage}");
                }
				return View("Error");
			}

            //_context.SaveChanges();
            //TempData["notice"] = "Contact created successfully";
            Console.WriteLine($"");
            return RedirectToAction(nameof(Index)); 
        }

        // GET: HomeController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if(id < 0)
            {
                return Redirect("~/Error");
            }
            else 
            { 
                return View(book.Contacts[id]); 
            }
		}

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,
            string surname, string name, string midname, int phone, string address, string description)
        {
            if(id < 0 || !ModelState.IsValid)
            {
				return Redirect(nameof(Error));
            }
            else 
            {
                using (var context = new ApplicationDbContext())
                {
                    var contact = book.Contacts[id];
                    contact.Surname = surname;
                    contact.Name = name;
                    contact.Midname = midname;
                    contact.Phone = phone;
                    contact.Address = address;
                    contact.Description = description;
                    context.SaveChanges();
                }
                return Redirect("~/Index");
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
                return Redirect("~/Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Error() 
        { 
            return View(); 
        }
    }
}
