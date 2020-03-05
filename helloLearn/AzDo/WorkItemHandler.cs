using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using helloLearn.Business;

namespace helloLearn.AzDo
{
    public class WorkItemHandler
    {
        

		public void ProcessWorkItem(int id)
		{
			Task<string> task = Task.Run(() => HttpHandler.GetWorkItemById(id));
			task.Wait();
			var result = task.Result;

			var witResult = ProcessJsonResponse(result);
			if (witResult != null)
			{
				var workItemType = witResult.value[0].fields.WorkItemType;
				switch (workItemType)
				{
					case "Module":
						{
							var moduleTitle = string.Empty;
							string[] unitTitles = null;
							var moduleUrl = GetModuleInformation(witResult, out moduleTitle, out unitTitles);


							ModuleHandler moduleHandler = new ModuleHandler(id,moduleUrl, moduleTitle, unitTitles);
							var myModle = moduleHandler.GenerateModule();

						}; break;
				}
			}
		}

		


		public ResponseWIT ProcessJsonResponse(string jsonInput)
		{
			var obj = JsonSerializer.Deserialize<ResponseWIT>(jsonInput);

			if (obj != null && obj.value.Length >= 1 && obj.value[0] != null)
				return obj;

			return null;
		}

		public string GetModuleInformation(ResponseWIT obj, out string moduleTitle, out string[] unitTitles)
		{
			moduleTitle = string.Empty;
			unitTitles = null;
			if (obj!=null)
			{
				moduleTitle = obj.value[0].fields.Title;


				unitTitles = obj.value[0].fields.ParseOutlineUnits().Split(new string[] { "-", "\n", "*" }, StringSplitOptions.RemoveEmptyEntries);

				return obj.value[0].url;
			}
			return string.Empty;
		}
		
	}

}
