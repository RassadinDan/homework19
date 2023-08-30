using Homework19.Views.Home;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace Homework19.Models
{
	public class ContactFactory
	{
		//private readonly ApplicationDbContext _context;
		public ContactFactory() 
		{
			//_context = context;
		}
		public Contact CreateContact([FromForm(Name = "CreateModel")]string surname, [FromForm(Name = "CreateModel")] string name,
			[FromForm(Name = "CreateModel")] string midname, [FromForm(Name = "CreateModel")] string phone, [FromForm(Name = "CreateModel")] string address, 
			[FromForm(Name = "CreateModel")] string description)
		{
			Contact contact;
			using (var context = new ApplicationDbContext())
			{
				int id = context.Contacts.Count();
				int tel = Convert.ToInt32(phone);
				contact = new Contact(id, surname, name, midname, tel, address, description);

				context.Contacts.Add(contact);
				context.SaveChanges();
			}
			return contact;
		}
	}
}
