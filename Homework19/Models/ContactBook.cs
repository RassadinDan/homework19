using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Homework19.Models
{
	public class ContactBook
	{
		public List<Contact> Contacts;

		private readonly ApplicationDbContext _context;
		public ContactBook(ApplicationDbContext context)
		{
			Contacts = new List<Contact>();
			_context = context;
		}

		public void Fill()
		{
			using (var context = new ApplicationDbContext())
			{
				var contacts = context.Contacts.ToList();
				Contacts = contacts;
			}
				//Contacts = _context.Contacts.Local.ToBindingList();
		}
	}
}
