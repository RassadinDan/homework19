using ContactWebAPI.Models;
using ContactWebAPI.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ContactWebAPI.Data
{
	public static class Repository
	{
		static List<Contact> data;

		static readonly ContactDBContext context;
		static Repository()
		{
			context = new ContactDBContext();
			data = context.Contacts.ToList<Contact>();
		}

		public static IEnumerable<Contact> GetAll() => data;

		public static void AddContact(Contact contact) 
		{
			contact.Id = data.Count;
			data.Add(contact);
		}

		public static void UpdateContact(Contact contact)
		{
			int id = contact.Id;
			data[id] = contact;
		}

		public static void RemoveContact(int id)
		{
			data.RemoveAt(id);
		}
	}
}
