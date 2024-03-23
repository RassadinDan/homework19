using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using ContactWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ContactWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DataController : ControllerBase
	{
		private readonly Repository _repository;
		public DataController(Repository repository)
		{
			_repository = repository;
		}

		[HttpGet("getcontacts")]
		public async Task<ActionResult<IEnumerable<Contact>>> Get()
		{
			var contacts = await _repository.GetAllAsync();
			return Ok(contacts);
		}

		[HttpGet("getone/{id}")]
		public async Task<ActionResult<Contact>> GetOne(int id) 
		{
			var contact = await _repository.GetByIdAsync(id);
			if(contact == null)
			{
				return NotFound();
			}
			return Ok(contact);
		}

		[HttpPost("addcontact")]
		[Authorize]
		public async Task<IActionResult> Post([FromBody] Contact contact)
		{
			await _repository.AddContactAsync(contact);
			return Ok();
		}

		[HttpPut("updatecontact/{id}")]
		[Authorize(Policy = "RequireAdministratorRole")]
		public async Task<IActionResult> Put(int id, [FromBody] Contact contact) 
		{
			if (id != contact.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.UpdateContactAsync(contact);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await ContactExists(contact.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpDelete("removecontact/{id}")]
		[Authorize(Policy = "RequireAdministratorRole")]
		public async Task<IActionResult> Delete(int id) 
		{
			var contact = await _repository.GetByIdAsync(id);
			if (contact == null)
			{
				return NotFound();
			}

			await _repository.RemoveContactAsync(id);
			return NoContent();
		}

		private async Task<bool> ContactExists(int id)
		{
			var contact = await _repository.GetByIdAsync(id);
			return contact != null;
		}
	}
}
