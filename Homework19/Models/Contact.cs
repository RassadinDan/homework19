using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework19.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required, FromForm]
        public string Surname { get; set; }

        [Required, FromForm]
        public string Name { get; set; }

        [Required, FromForm]
        public string Midname { get; set; }

        [Required, FromForm]
        public int Phone { get; set; }

        [Required, FromForm]
        public string Address { get; set; }

        [Required, FromForm]
        public string Description { get; set; }

        public Contact(int id, string surname, string name, string midname, int phone, string address, string description)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Midname = midname;
            Phone = phone;
            Address = address;
            Description = description;
        }
    }
}
