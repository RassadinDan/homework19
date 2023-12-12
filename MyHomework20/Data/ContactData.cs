using MyHomework20.DataContext;
using MyHomework20.Interfaces;
using MyHomework20.Models;

namespace MyHomework20.Data
{
	public class ContactData : IContactData
	{
		private readonly ContactDBContext _context;

		public ContactData(ContactDBContext context)
		{
			_context = context;
		}

		public IEnumerable<Contact> GetContacts()
		{
			return this._context.Contacts;
		}

		public void AddContact(Contact contact) 
		{
			_context.Contacts.Add(contact);
			_context.SaveChanges();
		}
	}
}
