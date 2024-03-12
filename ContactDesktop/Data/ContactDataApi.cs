using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ContactDesktop.Interfaces;
using ContactDesktop.Models;
using System;

namespace ContactDesktop.Data
{
	public class ContactDataApi : IContactData
	{
		private HttpClient _httpClient {  get; set; }

		public ContactDataApi()
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
			_httpClient = new HttpClient(handler);
		}

		public void AddContact(Contact contact)
		{
			string url = @"https://localhost:7062/api/data";

			var r = _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json")).Result;
			Console.Write(r);
		}

		public IEnumerable<Contact> GetContacts() 
		{
			string url = @"https://localhost:7062/api/data";

			string json = _httpClient.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
		}
		
		public Contact GetContactById(int id) 
		{
			string url = $@"https://localhost:7062/api/data/{id}";

			string json = _httpClient.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<Contact>(json);
		}

		public void Update(Contact contact)
		{
			int id = contact.Id;
			string url = $@"https://localhost:7062/api/data/{id}";

			var r = _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json")).Result;
			Console.Write(r);
		}

		public void Remove(int id)
		{
			string url = $@"https://localhost:7062/api/data/{id}";

			var r = _httpClient.DeleteAsync(url).Result;
			Console.Write(r);
		}
	}
}
