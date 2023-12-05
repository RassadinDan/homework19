using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHomework20.Models;

namespace MyHomework20.DataContext
{
    public class ContactDBContext : IdentityDbContext<User>
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>();
            builder.Entity<User>(options => options.HasKey("UserName"));
            builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            builder.Entity<IdentityUserRole<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ContactData_1;Integrated Security = true");
        }

        public ContactDBContext() { }

        public ContactDBContext(DbContextOptionsBuilder optionsBuilder) : base()
        {

        }
    }
}
