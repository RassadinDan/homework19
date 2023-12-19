using ContactDesktop.Models;
using System.Collections.Generic;

namespace ContactDesktop.Interfaces
{
	public interface IContactData
	{
		IEnumerable<Contact> GetContacts();
		void AddContact(Contact contact);
	}
}
