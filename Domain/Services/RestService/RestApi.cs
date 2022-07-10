namespace Domain.Services
{
    using Newtonsoft.Json;
    using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading;
	using System.Threading.Tasks;

	public static class RestApi
	{
		public static async Task<T> GetAsync<T>(string apiUrl, string token = null)
			where T : class
		{
			using (HttpClient client = new HttpClient())
			{
				client.Timeout = Timeout.InfiniteTimeSpan;

				try
				{
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					if (token != null)
					{
						client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
					}

					var response = await client.GetAsync(apiUrl);
					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var obj = JsonConvert.DeserializeObject<T>(content);
						return obj;
					}
					else
					{
						throw new Exception("Falha ao consultar a URL: " + apiUrl);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public static async Task<HttpResponseMessage> PostAsync(string url, object content = null, string token = null)
		{
			using (HttpClient client = new HttpClient())
			{
				string jsonString = JsonConvert.SerializeObject(content);
				//jsonString = jsonString.Replace("Sound", "sound");
				//jsonString = jsonString.Replace("Type", "type"); // type minusculos se não da erro 

				using (HttpContent httpContent = new StringContent(jsonString))
				{
					httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					if (token != null)
					{
						client.DefaultRequestHeaders.Add("X-API-Token", token);
						client.DefaultRequestHeaders.Add("X-Brad-Nonce", "");
						client.DefaultRequestHeaders.Add("X-API-Token", token);
						client.DefaultRequestHeaders.Add("X-API-Token", token);

					}

					return await client.PostAsync(url, httpContent);
				}
			}
		}

		public static async Task<HttpResponseMessage> PutAsync(string url, object content = null, string token = null)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					string jsonString = JsonConvert.SerializeObject(content);
					//jsonString = jsonString.Replace("Sound", "sound");
					//jsonString = jsonString.Replace("Type", "type"); // type minusculos se não da erro 

					using (HttpContent httpContent = new StringContent(jsonString))
					{
						httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
						if (token != null)
						{
							client.DefaultRequestHeaders.Add("X-API-Token", token);
						}

						return await client.PutAsync(url, httpContent);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw e;
			}
		}
	}
}
