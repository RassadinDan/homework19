using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ContactDesktop.Models;
using Newtonsoft.Json;

namespace ContactDesktop.Data
{
	public class UserDataApi
	{
		private HttpClient _httpClient;

		public UserDataApi()
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
			_httpClient = new HttpClient(handler);
		}

		public void Register(UserRegistration model)
		{
			var url = "https://localhost:7062/api/user/register";
			var r =_httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
				mediaType: "application/json")).Result;
			Console.WriteLine(r);
		}

		public async Task<bool> Login(UserLogin model)
		{
			var url = "https://localhost:7062/api/user/login";

			try { 

				var r = _httpClient.PostAsync(
					requestUri: url,
					content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
					mediaType: "application/json")).Result;
				var responseContent = await r.Content.ReadAsStringAsync();
				var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

				Session.UserName = model.UserName;
				Session.AuthToken = loginResponse.Token;
				Console.WriteLine($"{Session.UserName}, {Session.AuthToken}");
				return true; 
			}
			catch(HttpRequestException ex)
			{
				Console.WriteLine($"Ошибка при запросе к API: {ex.Message}");
			}
			return false;
		}

		public async Task<bool> Logout()
		{
			var url = "https://localhost:7062/api/user/logout";
			var r = await _httpClient.PostAsync(url, null);

			if(r.IsSuccessStatusCode)
			{
				Session.ClearSession();
				return true;
			}
			else
			{
				Console.WriteLine($"Ошибка выхода: {r.StatusCode}");
				return false;
			}
		}
	}

	public class LoginResponse
	{
		public string Token { get; set; }
	}
}
