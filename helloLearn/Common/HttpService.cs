using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace helloLearn.Common
{
    public class HttpService
    {
        //GetService
        public static async Task<string> GetService(string url, string patToken)
        {
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
					Convert.ToBase64String(
						System.Text.ASCIIEncoding.ASCII.GetBytes(
							string.Format("{0}:{1}", "", patToken))));

				using (HttpResponseMessage response = await client.GetAsync(url))
				{
					response.EnsureSuccessStatusCode();
					string responseBody = await response.Content.ReadAsStringAsync();

					return responseBody;
				}
			}
		}

		//PostService
		public static async Task<string> PostService(string url, string stringContent, string patToken)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
					Convert.ToBase64String(
						System.Text.ASCIIEncoding.ASCII.GetBytes(
							string.Format("{0}:{1}", "", patToken))));

				HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json-patch+json"); //media type is application/json-patch+json
				using (HttpResponseMessage response = await client.PostAsync(url, httpContent))
				{
					response.EnsureSuccessStatusCode();
					string responseBody = await response.Content.ReadAsStringAsync();

					return responseBody;

				}
			}
		}

		//PatchService
		public static async Task<string> PatchService(string url, string stringContent, string patToken)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
					Convert.ToBase64String(
						System.Text.ASCIIEncoding.ASCII.GetBytes(
							string.Format("{0}:{1}", "", patToken))));

				HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json-patch+json"); //media type is application/json-patch+json
				using (HttpResponseMessage response = await client.PatchAsync(url, httpContent))
				{
					response.EnsureSuccessStatusCode();
					string responseBody = await response.Content.ReadAsStringAsync();

					return responseBody;

				}
			}
		}

		//DeleteService
		public static async Task<string> DeleteService(string url, string patToken)
		{
			using(HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
					Convert.ToBase64String(
						System.Text.ASCIIEncoding.ASCII.GetBytes(
							string.Format("{0}:{1}", "", patToken))));

				using(HttpResponseMessage response = await client.DeleteAsync(url))
				{
					response.EnsureSuccessStatusCode();
					string responseBody = await response.Content.ReadAsStringAsync();

					return responseBody;
				}
			}
		}
	}
}
