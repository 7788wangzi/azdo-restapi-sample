using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using helloLearn.Business;
using helloLearn.Learn;

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
							var myModule = moduleHandler.GenerateModule();

							UpdateModuleWorkItem(myModule);
							CreateModuleUnits(myModule);
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

		private void UpdateModuleWorkItem(Module myModule)
		{
			List<JsonPatch> jsonPatchDocument = new List<JsonPatch>();

			JsonPatch uidPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.UID",
				value = $"{myModule.UID}"
			};
			jsonPatchDocument.Add(uidPatch);


			//Custom.badge
			JsonPatch badgeUidPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.badge",
				value = $"{myModule.UID}.badge"
			};
			jsonPatchDocument.Add(badgeUidPatch);

			JsonPatch moduleFolderPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.TargetFolder",
				value = $"{myModule.ModuleFolder}"
			};
			jsonPatchDocument.Add(moduleFolderPatch);
			
			var jsonDocument = JsonSerializer.Serialize(jsonPatchDocument);

			Task<string> task = Task.Run(() => HttpHandler.UpdateWorkItem(myModule.Id, jsonDocument));
			task.Wait();
			var result = task.Result;
			Console.WriteLine("Updated: "+myModule.Id);

		}

		public void CreateModuleUnits(Learn.Module module)
		{
			foreach (var unit in module.Units)
			{
				CreateUnitWorkItem(unit, module.Url);
			}
		}
		public void CreateUnitWorkItem(Learn.Unit unit, string moduleURL)
		{
			List<JsonPatch> jsonPatchDocument = new List<JsonPatch>();			

			JsonPatch titlePatch = new JsonPatch
			{
				op = "add",
				path = "/fields/System.Title",
				value = $"{unit.Title}"
			};
			jsonPatchDocument.Add(titlePatch);

			//Custom.content
			JsonPatch filePathPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.content",
				value = $"{unit.FileName}.md"
			};
			jsonPatchDocument.Add(filePathPatch);

			//Custom.UID
			JsonPatch uidPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.UID",
				value = $"{unit.UID}"
			};
			jsonPatchDocument.Add(uidPatch);

			JsonPatch relationPatch = new JsonPatch
			{
				op = "add",
				path = "/relations/-",
				value = new Link
				{
					rel = "System.LinkTypes.Hierarchy-Reverse",
					url = $"{moduleURL}",
					attributes = new AzDo.Attribute
					{
						comment = "Comment for link"
					}
				}
			};
			jsonPatchDocument.Add(relationPatch);
			var jsonDocument = JsonSerializer.Serialize(jsonPatchDocument);

			Task<string> task = Task.Run(() => HttpHandler.CreateWorkItem("Unit", jsonDocument));
			task.Wait();
			var result = task.Result;

			MyWorkItem myWorkItem = JsonSerializer.Deserialize<MyWorkItem>(result);
			Console.WriteLine("Created unit: "+myWorkItem.id);
		}
		
	}

}
