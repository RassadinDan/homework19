using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ModelLibrary.Auth.Dto;
using System.Net.Http.Headers;

namespace MyHomework20.Models
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> RegisterAsync(RegistrationRequest model)
		{
			var url = "https://localhost:7062/api/user/register";
			var r = await _httpClient.PostAsJsonAsync(url, model);
			Console.WriteLine(r.StatusCode);
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
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
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
