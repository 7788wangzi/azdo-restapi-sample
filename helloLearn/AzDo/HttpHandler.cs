using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using helloLearn.Common;

namespace helloLearn.AzDo
{
    public class HttpHandler
    {
		public static async Task<string> GetChildsOfModule(int id)
		{
			try
			{
				var personalaccesstoken = WorkItemHandler.witPatToken;
				string httpUrl = string.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems?ids={0}&$expand=Relations&api-version=5.0", id);
				string result = await HttpService.GetService(httpUrl, personalaccesstoken);

				return result;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return string.Empty;
			}
		}
		public static async Task<string> GetWorkItemById(int id)
		{
			try
			{				
				var personalaccesstoken = WorkItemHandler.witPatToken;
				string httpUrl = string.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems?ids={0}&fields=System.Id,System.Title,System.WorkItemType,Custom.outline_units,Custom.TargetReleaseDate,Custom.gitPath,Custom.md_ms_author,Custom.md_ms_date,Custom.md_ms_prod,System.AreaPath,System.IterationPath,Custom.TargetRepo&api-version=5.0", id);
								
				string result = await HttpService.GetService(httpUrl, personalaccesstoken);
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return string.Empty;
			}
		}

		public static async Task<string> CreateWorkItem(string workItemType, string jsonPatchDocument)
		{
			try
			{
				var personalaccesstoken = WorkItemHandler.witPatToken;
				string url = String.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems/${0}?api-version=5.0&validateOnly=false&bypassRules=true", workItemType);

				string result = await HttpService.PostService(url, jsonPatchDocument, personalaccesstoken);

				return result;
				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return string.Empty;
			}
		}

		public static async Task<string> UpdateWorkItem(int id, string jsonPatchDocument)
		{
			try
			{
				var personalaccesstoken = WorkItemHandler.witPatToken;
				string url = String.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems/{0}?api-version=5.0&validateOnly=false&bypassRules=true", id);

				string result = await HttpService.PatchService(url, jsonPatchDocument, personalaccesstoken);

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return string.Empty;
			}
		}

		public static async Task<string> DeleteWorkItem(int id)
		{
			try
			{
				var personalaccesstoken = WorkItemHandler.witPatToken;
				string url = string.Format(@"https://dev.azure.com/microsoftdigitallearning/courseware/_apis/wit/workitems/{0}?api-version=5.0", id);
				string result = await HttpService.DeleteService(url, personalaccesstoken);

				return result;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return string.Empty;
			}
		}
	}
}
