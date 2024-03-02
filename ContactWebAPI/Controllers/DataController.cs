using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using ContactWebAPI.Data;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DataController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Contact> Get()
		{
			return Repository.GetAll();
		}

		[HttpGet("{id}")]
		//[Route("api/data/get/{id}")]
		public Contact GetOne(int id) 
		{
			return Repository.GetById(id);
		}

		[HttpPost]
		public void Post([FromBody] Contact contact)
		{
			Repository.AddContact(contact);
		}

		[HttpPut("{id}")]
		public void Put([FromBody] Contact contact) 
		{
			Repository.UpdateContact(contact);
		}

		[HttpDelete("{id}")]
		public void Delete(int id) 
		{
			Repository.RemoveContact(id);
		}
	}
}
