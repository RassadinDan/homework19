using ContactWebAPI.Models;

namespace ContactWebAPI.Data
{
	public static class Repository
	{
		static List<Contact> data;

		static Repository()
		{
			data = new List<Contact>();
		}

		public static IEnumerable<Contact> GetAll() => data;

		public static void AddContact(Contact contact) 
		{
			contact.Id = data.Count;
			data.Add(contact);
		}
	}
}
