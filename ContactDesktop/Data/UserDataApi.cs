using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ContactDesktop.Models;
using Newtonsoft.Json;
using ContactDesktop.Auth.Dto;

namespace ContactDesktop.Data
{
	public class UserDataApi
	{
		private HttpClient _httpClient;

		public UserDataApi()
		{
			_httpClient = new HttpClient();
		}

		public async Task Register(RegistrationRequest model)
		{
			var url = "https://localhost:7062/api/user/register";
			var r = await _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
				mediaType: "application/json"));
			
		}

		public async Task<bool> Login(LoginRequest model)
		{
			var url = "https://localhost:7062/api/user/login";

			try { 

				var r = await _httpClient.PostAsync(
					requestUri: url,
					content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
					mediaType: "application/json"));
				var responseContent = await r.Content.ReadAsStringAsync();
				var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

				AuthSession.User = loginResponse.User;
				AuthSession.Token = loginResponse.Token;
				AuthSession.IsAuthenticated = true;
				Console.WriteLine($"{AuthSession.User.UserName}, {AuthSession.Token}");
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
				AuthSession.ClearSession();
				return true;
			}
			else
			{
				Console.WriteLine($"Ошибка выхода: {r.StatusCode}");
				return false;
			}
		}
	}
}
