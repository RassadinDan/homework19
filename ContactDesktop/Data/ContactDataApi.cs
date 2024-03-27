using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ContactDesktop.Interfaces;
using ContactDesktop.Models;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ContactDesktop.Data
{
	public class ContactDataApi : IContactData
	{
		private HttpClient _httpClient {  get; set; }

		public ContactDataApi()
		{
			_httpClient = new HttpClient();
			if(AuthSession.IsAuthenticated == true)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthSession.Token);
			}
		}

		public async Task AddContact(Contact contact)
		{
			string url = @"https://localhost:7062/api/data/addcontact";

			var r = await _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json"));
			Console.Write($"{r.StatusCode}===> {r.IsSuccessStatusCode}\n");
		}

		public async Task<IEnumerable<Contact>> GetContacts()
		{
			string url = @"https://localhost:7062/api/data/getcontacts";

			var json = await _httpClient.GetStringAsync(url);
			return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
		}
		
		public async Task<Contact> GetContactById(int id) 
		{
			string url = $@"https://localhost:7062/api/data/getone/{id}";

			string json = await _httpClient.GetStringAsync(url);
			return JsonConvert.DeserializeObject<Contact>(json);
		}

		public async Task Update(Contact contact)
		{
			int id = contact.Id;
			string url = $@"https://localhost:7062/api/data/updatecontact/{id}";

			var r = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json"));
			Console.Write($"{r.StatusCode}===> {r.IsSuccessStatusCode}\n");
		}

		public async Task Remove(int id)
		{
			string url = $@"https://localhost:7062/api/data/removecontact/{id}";

			var r = await _httpClient.DeleteAsync(url);
			Console.Write($"{r.StatusCode}===> {r.IsSuccessStatusCode}\n");
		}
	}
}
