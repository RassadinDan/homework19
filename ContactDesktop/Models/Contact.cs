//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace ContactDesktop.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Midname { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public Contact() { }

		public override string ToString()
		{
            return $"{Id}. {Surname} {Name} {Midname}";
		}
	}
}
