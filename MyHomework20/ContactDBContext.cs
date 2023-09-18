using Microsoft.EntityFrameworkCore;
using MyHomework20.Models;

namespace MyHomework20
{
	public class ContactDBContext : DbContext
	{
		public DbSet<Contact> Contacts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Contact>();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactData;Integrated Security=True
			optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ContactData;Integrated Security = true");
		}

		public ContactDBContext() { }

		public ContactDBContext(DbContextOptionsBuilder optionsBuilder) : base()
		{

		}
	}
}
