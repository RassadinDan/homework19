namespace Homework19.Models
{
	public class ContactFactory
	{
		private readonly ApplicationDbContext _context;
		public ContactFactory(ApplicationDbContext context) 
		{
			_context = context;
		}
		public Contact CreateContact()
		{
			int id = _context.Contacts.Count();
			var contact = new Contact(/*id, surname, name, midname, phone, address, description*/);
			contact.Id = id;
			return contact;
		}
	}
}
