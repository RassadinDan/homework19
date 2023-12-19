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
			contact.Id = data.Count+1;
			data.Add(contact);
			context.SaveChanges();
			//Save();
		}

		public static void UpdateContact(Contact contact)
		{
			data[contact.Id-1] = contact;
			context.SaveChanges();
			//Save();
		}

		public static void RemoveContact(int id)
		{
			data.RemoveAt(id);
			context.SaveChanges();
			//Save();
		}

		public static void Save()
		{
			foreach(Contact contact in data)
			{
				if(context.Contacts.Contains(contact)) 
				{
					context.Contacts.Update(contact);
				}
				else 
				{
					context.Contacts.Add(contact); 
				}
			}

			using(var trans = context.Database.BeginTransaction())
			{
				context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Contacts] ON");
				context.SaveChanges();
				context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Contacts] OFF");
				trans.Commit();
			}
		}
	}
}
