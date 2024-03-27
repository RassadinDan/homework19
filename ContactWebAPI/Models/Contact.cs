using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactWebAPI.Models
{
    [Table("Contacts")]
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

        public Contact()
        {

        }

    }
}
