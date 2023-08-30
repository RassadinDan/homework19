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

		public ContactBook()
		{
			Contacts = new List<Contact>();
			factory= new ContactFactory();
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
