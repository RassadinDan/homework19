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

        [Required, FromForm(Name = "surname")]
        public string Surname { get; set; }

        [Required, FromForm(Name = "name")]
        public string Name { get; set; }

        [Required, FromForm(Name = "midname")]
        public string Midname { get; set; }

        [Required, FromForm(Name = "phone")]
        public int Phone { get; set; }

        [Required, FromForm(Name = "address")]
        public string Address { get; set; }

        [Required, FromForm(Name = "description")]
        public string Description { get; set; }

        //public Contact(int id, string surname, string name, string midname, int phone, string address, string description)
        //{
        //    Id = id;
        //    Surname = surname;
        //    Name = name;
        //    Midname = midname;
        //    Phone = phone;
        //    Address = address;
        //    Description = description;
        //}
    }
}
