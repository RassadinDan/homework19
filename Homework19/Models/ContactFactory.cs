using System.Net;
using System.Numerics;

namespace Homework19.Models
{
	public class ContactFactory
	{
		private readonly ApplicationDbContext _context;
		public ContactFactory(ApplicationDbContext context) 
		{
			_context = context;
		}
		public Contact CreateContact(string surname, string name, string midname, int phone, string address, string description)
		{
			int id = _context.Contacts.Count();
			var contact = new Contact();
			contact.Id = id;
			contact.Surname = surname;
			contact.Name = name;
			contact.Midname = midname;
			contact.Phone = phone;
			contact.Address = address;
			contact.Description = description;
			_context.Contacts.Add(contact);
			return contact;
		}
	}
}
