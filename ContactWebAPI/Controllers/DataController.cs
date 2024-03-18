using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using ContactWebAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DataController : ControllerBase
	{
		[HttpGet("getcontacts")]
		public IEnumerable<Contact> Get()
		{
			return Repository.GetAll();
		}
		[HttpGet("getone/{id}")]
		public Contact GetOne(int id) 
		{
			return Repository.GetById(id);
		}

		[HttpPost("addcontact")]
		[Authorize(Roles = "User")]
		public void Post([FromBody] Contact contact)
		{
			Repository.AddContact(contact);
		}

		[HttpPut("updatecontact/{id}")]
		[Authorize(Roles = "Administrator")]
		public void Put([FromBody] Contact contact) 
		{
			Repository.UpdateContact(contact);
		}

		[HttpDelete("removecontact/{id}")]
		[Authorize(Roles = "Administrator")]
		public void Delete(int id) 
		{
			Repository.RemoveContact(id);
		}
	}
}
