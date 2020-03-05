using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace helloLearn.AzDo
{
    public class HttpHandler
    {
		public static async Task<string> GetWorkItemById(int id)
		{
			try
			{
				var personalaccesstoken = "szrd36f63juesylpw3cwokgybzhhvtqtnkbpa7thlrol6ccmmm5a";

				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Add(
						new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
						Convert.ToBase64String(
							System.Text.ASCIIEncoding.ASCII.GetBytes(
								string.Format("{0}:{1}", "", personalaccesstoken))));

					string httpUrl = string.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems?ids={0}&fields=System.Id,System.Title,System.WorkItemType,Custom.outline_units,Custom.TargetReleaseDate,Custom.gitPath,Custom.md_ms_author,Custom.md_ms_date&api-version=5.0", id);

					using (HttpResponseMessage response = await client.GetAsync(httpUrl))
					{
						response.EnsureSuccessStatusCode();
						string responseBody = await response.Content.ReadAsStringAsync();

						return responseBody;

						Console.WriteLine(responseBody);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return string.Empty;
			}
			finally
			{

			}
		}
	}
}
