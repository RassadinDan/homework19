using ContactWebAPI.Models;
using ContactWebAPI.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ContactWebAPI.Data
{
	public class Repository
	{
		private readonly ContactDBContext _context;

		public Repository(ContactDBContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Contact>> GetAllAsync()
		{
			return await _context.Contacts.ToListAsync();
		}

		public async Task<Contact> GetByIdAsync(int id)
		{
			return await _context.Contacts.FindAsync(id);
		}

		public async Task AddContactAsync(Contact contact)
		{
			_context.Contacts.Add(contact);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateContactAsync(Contact contact)
		{
			_context.Contacts.Update(contact);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveContactAsync(int id)
		{
			var contact = await _context.Contacts.FindAsync(id);
			if (contact != null)
			{
				_context.Contacts.Remove(contact);
				await _context.SaveChangesAsync();
			}
		}
	}

}
