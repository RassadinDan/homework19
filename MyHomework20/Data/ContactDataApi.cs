using MyHomework20.Interfaces;
using MyHomework20.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace MyHomework20.Data
{
	public class ContactDataApi : IContactData
	{
		private HttpClient _httpClient {  get; set; }

		public ContactDataApi()
		{
			_httpClient = new HttpClient();
			if (AuthSession.IsAuthenticated == true)
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
		}

		public async Task<IEnumerable<Contact>> GetContacts() 
		{
			string url = @"https://localhost:7062/api/data/getcontacts";

			string json = await _httpClient.GetStringAsync(url);
			return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
		}
		
		public async Task<Contact> GetContactById(int id) 
		{
			string url = @$"https://localhost:7062/api/data/getone/{id}";

			string json = await _httpClient.GetStringAsync(url);
			var contact = JsonConvert.DeserializeObject<Contact>(json);
			return contact;
		}

		public async Task Update(Contact contact)
		{
			int id = contact.Id;
			string url = @$"https://localhost:7062/api/data/updatecontact/{id}";

			var r = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json"));
		}

		public async Task Remove(int id)
		{
			string url = @$"https://localhost:7062/api/data/removecontact/{id}";

			var r = await _httpClient.DeleteAsync(url);
		}
	}
}
