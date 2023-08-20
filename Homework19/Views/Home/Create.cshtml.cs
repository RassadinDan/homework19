using Homework19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework19.Views.Home
{
    public class CreateModel : PageModel
    {
        ContactFactory factory = new ContactFactory(new ApplicationDbContext());
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var surname = new FromFormAttribute();
            surname.Name = "surname";
            var name = new FromFormAttribute();
            name.Name= "name";
            var midname = new FromFormAttribute();
            midname.Name= "midname";
            var phone = new FromFormAttribute();
            phone.Name= "phone";
            var address = new FromFormAttribute();
            address.Name= "address";
            var description = new FromFormAttribute();
            description.Name= "description";

            Contact contact = factory.CreateContact(surname, name, midname, phone, address, description);
            
            var validator = new ContactValidator();
            validator.Validate(contact);

            //return RedirectToAction("Index", "Home");
        }
    }
}
