using MyHomework20.Models;

namespace MyHomework20.Interfaces
{
	public interface IContactData
	{
		Task<IEnumerable<Contact>> GetContacts();
		Task AddContact(Contact contact);
	}
}
