using System.ComponentModel.DataAnnotations.Schema;

namespace Homework19.Models
{
    [Table("Contacts")]
    public class Contact
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Midname { get; set; }

        public int Phone { get; set; }

        public string Address { get; set; }

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
