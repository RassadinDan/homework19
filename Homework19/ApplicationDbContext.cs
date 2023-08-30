using Homework19.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework19
{
    public class ApplicationDbContext : DbContext
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
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}