using ContactDesktop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactDesktop.Interfaces
{
	public interface IContactData
	{
		Task<IEnumerable<Contact>> GetContacts();
		Task AddContact(Contact contact);
	}
}
