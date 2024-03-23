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
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
			_httpClient = new HttpClient(handler);
		}

		public void AddContact(Contact contact)
		{
			string url = @"https://localhost:7062/api/data/addcontact";

			var r = _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json")).Result;
		}

		public IEnumerable<Contact> GetContacts() 
		{
			string url = @"https://localhost:7062/api/data/getcontacts";

			string json = _httpClient.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
		}
		
		public Contact GetContactById(int id) 
		{
			string url = @$"https://localhost:7062/api/data/getone/{id}";

			if (AuthSession.IsAuthenticated == true)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthSession.Token);
			}

			string json = _httpClient.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<Contact>(json);
		}

		public void Update(Contact contact)
		{
			int id = contact.Id;
			string url = @$"https://localhost:7062/api/data/updatecontact/{id}";

			if (AuthSession.IsAuthenticated == true)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthSession.Token);
			}

			var r = _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json")).Result;
		}

		public void Remove(int id)
		{
			string url = @$"https://localhost:7062/api/data/removecontact/{id}";

			if (AuthSession.IsAuthenticated == true)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthSession.Token);
			}

			var r = _httpClient.DeleteAsync(url).Result;
		}
	}
}
