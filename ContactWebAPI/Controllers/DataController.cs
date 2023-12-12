﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactWebAPI.Models;
using ContactWebAPI.Data;

namespace ContactWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Contact> Get()
		{
			return Repository.GetAll();
		}

		[HttpPost]
		public void Post([FromBody] Contact contact)
		{
			Repository.AddContact(contact);
		}
	}
}
