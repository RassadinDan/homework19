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
			_httpClient = new HttpClient();
		}

		public void Register(UserRegistration model)
		{
			//var model = new UserRegistration(){ UserName = username, Password = password, ConfirmPassword = confpswd };
			var url = "https://localhost:7062/api/user/register";
			var r =_httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
				mediaType: "application/json")).Result;
			Console.WriteLine(r);
		}

		public async Task<string> Login(UserLogin model)
		{
			//var model = new UserLogin() { UserName = username,Password = password };
			var url = "https://localhost:7062/api/user/login";
			var r = _httpClient.PostAsync(
				requestUri: url,
				content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
				mediaType: "application/json")).Result;
			var username = await r.Content.ReadAsStringAsync();
			Console.WriteLine(username);
			return username;
		}

		public async Task<bool> Logout() 
		{
			var url = "https://localhost:7062/api/user/logout";
			var r = await _httpClient.PostAsync(url, null);

			if(r.IsSuccessStatusCode) 
			{
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
