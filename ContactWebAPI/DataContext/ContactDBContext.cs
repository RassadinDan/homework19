using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactWebAPI.Models;
using ModelLibrary.Auth;

namespace ContactWebAPI.DataContext
{
    public class ContactDBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users {  get; set; } 

        public ContactDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>();
            builder.Entity<User>(options => options.HasKey("UserName"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public ContactDBContext() { }

        public ContactDBContext(DbContextOptionsBuilder optionsBuilder) : base()
        {

        }
    }
}
