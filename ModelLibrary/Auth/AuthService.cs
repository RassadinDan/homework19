using ModelLibrary.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Auth
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
			var url = "https://localhost:7044/api/auth/register";
			var r = await _httpClient.PostAsJsonAsync(url, model);

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
			var url = "https://localhost:7044/api/auth/login";
			var r = await _httpClient.PostAsJsonAsync(url, model);

			if(r.IsSuccessStatusCode)
			{
				var authResponse = await r.Content.ReadFromJsonAsync<LoginResponse>();
				//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
				return authResponse;
			}
			return new LoginResponse()
			{
				User = null,
				Token = ""
			};
		}

		public async Task<bool> LogoutAsync()
		{
			var url = "https://localhost:7044/api/auth/logout";
			var r = await _httpClient.PostAsync(url, null);

			if(r.IsSuccessStatusCode)
			{
				_httpClient.DefaultRequestHeaders.Authorization = null;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
