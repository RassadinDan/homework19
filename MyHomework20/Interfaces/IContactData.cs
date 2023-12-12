using MyHomework20.Models;

namespace MyHomework20.Interfaces
{
	public interface IContactData
	{
		IEnumerable<Contact> GetContacts();
		void AddContact(Contact contact);
	}
}
