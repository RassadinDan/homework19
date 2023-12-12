using MyHomework20.Interfaces;
using MyHomework20.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyHomework20.Data
{
	public class ContactDataApi : IContactData
	{
		private HttpClient _httpClient {  get; set; }

		public ContactDataApi()
		{
			_httpClient = new HttpClient();
		}

		public void AddContact(Contact contact)
		{
			string url = @"https://localhost:7085/api/data";

			var r = _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
				mediaType: "application/json")).Result;
		}

		public IEnumerable<Contact> GetContacts() 
		{
			string url = @"https://localhost:7085/api/data";

			string json = _httpClient.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
		}
	}
}
