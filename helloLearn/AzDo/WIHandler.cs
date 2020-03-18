using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using helloLearn.Learn;

namespace helloLearn.AzDo
{
    public class WIHandler
    {
        public static string PersonalAccessToken { get; set; }
        public WIHandler(string token)
        {
            PersonalAccessToken = token;
        }
        /// <summary>
        /// Handle response of:
        /// 1. Create work item.
        /// 2. Update a work item field
        /// </summary>
        /// <param name="jsonResponse"></param>
        /// <returns></returns>
        public IWorkItem ConvertToWorkItem(string jsonResponse)
        {
            if (string.IsNullOrEmpty(jsonResponse))
                return null;
            var obj = JsonSerializer.Deserialize<ResponseWIT>(jsonResponse);

            if (obj != null && obj.value.Length >= 1 && obj.value[0] != null)
            {
                IWorkItem myWorkItem = null;
                if(obj.value[0].fields.WorkItemType =="Learning Path")
                {
                    myWorkItem = new LearningPath();
                }
                else if(obj.value[0].fields.WorkItemType == "Module")
                {
                    myWorkItem = new Module();
                }

                myWorkItem.Id = obj.value[0].fields.Id;
                myWorkItem.WorkItemType = obj.value[0].fields.WorkItemType;
                myWorkItem.Title = obj.value[0].fields.Title;
                myWorkItem.AreaPath = obj.value[0].fields.AreaPath;
                myWorkItem.IteractionPath = obj.value[0].fields.IterationPath;
                myWorkItem.MsAuthor = obj.value[0].fields.MS_Author;
                myWorkItem.MsDate = obj.value[0].fields.MS_Date;
                myWorkItem.MsProduct = obj.value[0].fields.MS_Product;
                myWorkItem.OutlineStructure = obj.value[0].fields.outline_Units;
                myWorkItem.GitPath = obj.value[0].fields.GitPath;

                return myWorkItem;
            }
            return null;
        }

        /// <summary>
        /// Handle response of:
        /// 1. Get list of work items by id.
        /// </summary>
        /// <param name="jsonResponse"></param>
        /// <returns></returns>
        public IWorkItem[] ConvertToWorkItems(string jsonResponse)
        {
            return null;
        }

        public string ProcessRequest(IWorkItem workItem, RequestType type, out IWorkItem[] workItems)
        {
            workItems = null; //output the result object
            //return string.Empty; //return status code
            //If 
            switch(type)
            {
                case RequestType.GetWorkItem:
                    {
                        //Call HttpHandler.GetChildsOfModule(int id), pass in workItem.Id
                        Task<string> task = Task.Run(() => HttpHandler.GetWorkItemById(workItem.Id));
                        task.Wait();
                        var result = task.Result;

                        //Call ConvertToWorkItems(json), return the Work Item object, either a Learning Path, or Module, or Unit
                        ConvertToWorkItem(result);
                    }; break;
                case RequestType.CreateWorkItem:
                    {
                        //Call GenerateJsonPatchDocument(IWorkItem workItem) to create jsonPatchDocument

                        //Call HttpHandler.CreateWorkItem(string workItemType, string jsonPatchDocument)

                        //Call ConvertToWorkItem(json), return the Work Item object, either a Learning Path, or Module, or Unit
                    }; break;
                case RequestType.UpdateWorkItem:
                    {
                        // Call GenerateJsonPatchDocument(IWorkItem workItem) to create jsonPatchDocument

                        //Call HttpHandler.UpdateWorkItem(int id, string jsonPatchDocument) to update the work item, either a Learning Path, or Module
                    };break;


            }
            return null;
        }
    }

    public enum RequestType
    {
        GetWorkItem =1,
        CreateWorkItem,
        DeleteWorkItem,
        UpdateWorkItem
    }
}
