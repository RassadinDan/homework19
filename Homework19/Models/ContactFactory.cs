﻿namespace Homework19.Models
{
	public class ContactFactory
	{
		private readonly ApplicationDbContext _context;
		public ContactFactory(ApplicationDbContext context) 
		{
			_context = context;
		}
		public Contact CreateContact(string surname, string name, string midname, 
			int phone, string address, string description)
		{
			int id = _context.Contacts.Count();
			return new Contact(id, surname, name, midname, phone, address, description);
		}
	}
}
