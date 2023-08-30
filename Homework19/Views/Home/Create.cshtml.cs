using Homework19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework19.Views.Home
{
    public class CreateModel : PageModel
    {
        ContactFactory factory = new ContactFactory();

        public Contact contact;

        public void OnGet()
        {
        }

        public void OnPost(string surname, string name, string midname, string phone, string address, string description)
        {
            contact = factory.CreateContact(surname, name, midname, phone, address, description);
        }
    }
}
