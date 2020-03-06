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
		static DateTime witMSDate = new DateTime();
		static string witMSAuthor = string.Empty;
		static string witMSProd = string.Empty;
		static string witAreaPath = string.Empty;
		static string witIterationPath = string.Empty;


		public void ProcessWorkItem(int id)
		{
			Task<string> task = Task.Run(() => HttpHandler.GetWorkItemById(id));
			task.Wait();
			var result = task.Result;

			var witResult = ProcessJsonResponse(result);
			if (witResult != null)
			{
				var workItemType = witResult.value[0].fields.WorkItemType;
				witMSDate = witResult.value[0].fields.TargetReleaseDate;
				witMSAuthor = witResult.value[0].fields.MS_Author;
				witMSProd = witResult.value[0].fields.MS_Product;
				witAreaPath = witResult.value[0].fields.AreaPath;
				witIterationPath = witResult.value[0].fields.IterationPath;

				var repository = witResult.value[0].fields.Repository;

				switch (workItemType)
				{
					case "Module":
						{
							Console.WriteLine("This is a Module work item.");
							var moduleTitle = string.Empty;
							string[] unitTitles = null;
							var moduleUrl = GetModuleInformation(witResult, out moduleTitle, out unitTitles);
							System.Threading.Thread.Sleep(2000);
							Console.WriteLine("Module title: "+moduleTitle);
							Console.WriteLine("Unit titles: ");
							foreach (var u in unitTitles)
							{
								Console.WriteLine(u);
							}

							System.Threading.Thread.Sleep(3000);
							ModuleHandler moduleHandler = new ModuleHandler(id, moduleUrl, repository, moduleTitle, unitTitles);
							var myModule = moduleHandler.GenerateModule();

							Console.WriteLine("Update module with UID, target folder");
							System.Threading.Thread.Sleep(2000);
							UpdateModuleWorkItem(myModule);

							Console.WriteLine("Create unit work items");
							System.Threading.Thread.Sleep(2000);
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

			//Custom.md_ms_date
			JsonPatch msDatePatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.md_ms_date",
				value = $"{witMSDate}"
			};
			jsonPatchDocument.Add(msDatePatch);


			//Custom.md_ms_author
			JsonPatch msAuthorPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.md_ms_author",
				value = $"{witMSAuthor}"
			};
			jsonPatchDocument.Add(msAuthorPatch);

			//Custom.md_ms_prod
			JsonPatch msProdPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/Custom.md_ms_prod",
				value = $"{witMSProd}"
			};
			jsonPatchDocument.Add(msProdPatch);

			//System.AreaPath
			JsonPatch areaPathPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/System.AreaPath",
				value = $"{witAreaPath}"
			};
			jsonPatchDocument.Add(areaPathPatch);

			//System.IterationPath
			JsonPatch iterationPathPatch = new JsonPatch
			{
				op = "add",
				path = "/fields/System.IterationPath",
				value = $"{witIterationPath}"
			};
			jsonPatchDocument.Add(iterationPathPatch);

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
