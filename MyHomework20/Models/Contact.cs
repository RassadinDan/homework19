using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHomework20.Models
{
    [Table("Contacts")]
    //[Bind("Surname", "Name", "Midname", "Phone", "Address", "Description")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [FromForm]
        public string Surname { get; set; }

        [FromForm]
        public string Name { get; set; }

        [FromForm]
        public string Midname { get; set; }

        [MaxLength(6)]
        [FromForm]
        public string Phone { get; set; }

        [FromForm]
        public string Address { get; set; }

        [FromForm]
        public string Description { get; set; }

        public Contact(/*int id, string surname, string name, string midname, int phone, string address, string description*/)
        {
            //Id = id;
            //Surname = surname;
            //Name = name;
            //Midname = midname;
            //Phone = phone;
            //Address = address;
            //Description = description;
        }

    }
}
