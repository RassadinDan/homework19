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

		public static Contact GetById(int id)
		{
			return data.Find(contact => contact.Id == id);
		}

		public static void AddContact(Contact contact) 
		{
			contact.Id = data.Count;
			data.Add(contact);
			//context.SaveChanges();
		}

		public static void UpdateContact(Contact contact)
		{
			data.Insert(contact.Id-1, contact);
			//context.SaveChanges();
		}

		public static void RemoveContact(int id)
		{
			data.RemoveAt(id);
			//context.SaveChanges();
		}
	}
}
