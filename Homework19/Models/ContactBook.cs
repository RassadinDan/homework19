using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Homework19.Models
{
	public class ContactBook
	{
		public List<Contact> Contacts;

		public ContactFactory factory;

		private readonly ApplicationDbContext _context;
		public ContactBook(ApplicationDbContext context)
		{
			Contacts = new List<Contact>();
			_context = context;
			factory= new ContactFactory(_context);
		}

		/// <summary>
		/// Заполнение коллекции контактами из базы данных.
		/// </summary>
		public void Fill()
		{
			using (var context = new ApplicationDbContext())
			{
				var contacts = context.Contacts.ToList();
				Contacts = contacts;
			}
		}
	}
}
