using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Web.ApiService
{
	public class ApiService<T> : IApiService<T>
	{
		public string Method { get; set; }

		public HttpClient HttpClient { get; set; }

		public async Task<T> AddAsync(T dto)
		{
			var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

			var response = await HttpClient.PostAsync($"{Method}", stringContent);

			if (response.IsSuccessStatusCode)
			{
				dto = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
				return dto;
			}

			return default;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			IEnumerable<T> dtos = null;

			var response = await HttpClient.GetAsync($"{Method}");

			if (response.IsSuccessStatusCode)
			{
				dtos = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());
			}

			return dtos;
		}

		public async Task<bool> Remove(int id)
		{
			var response = await HttpClient.DeleteAsync($"{Method}/{id}");

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			return false;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var response = await HttpClient.GetAsync($"{Method}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var categoryDto = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

				return categoryDto;
			}

			return default;
		}

		public async Task<bool> Update(T dto)
		{
			var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

			var response = await HttpClient.PutAsync($"{Method}", stringContent);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			return false;
		}
	}
}
