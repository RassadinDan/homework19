using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ModelLibrary.Auth.Dto;

namespace MyHomework20.Models
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
			_httpClient = httpClient;
		}

		public async Task<bool> RegisterAsync(RegistrationRequest model)
		{
			var url = "https://localhost:7062/api/user/register";
			var r = await _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
				mediaType: "application/json"));
			Console.WriteLine(r);
			if(r.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task<LoginResponse> LoginAsync(LoginRequest model)
		{
			var url = "https://localhost:7062/api/user/login";

			var r = await _httpClient.PostAsJsonAsync(url, model);

			if(r.IsSuccessStatusCode==true)
			{
				var authResponse = await r.Content.ReadFromJsonAsync<LoginResponse>();
				return authResponse;
			}
			return new LoginResponse
			{
				User = null,
				Token = ""
			};
		}

		public async Task<bool> LogoutAsync()
		{
			var url = "https://localhost:7062/api/user/logout";
			var r = await _httpClient.PostAsync(url, null);

			if (r.IsSuccessStatusCode)
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
