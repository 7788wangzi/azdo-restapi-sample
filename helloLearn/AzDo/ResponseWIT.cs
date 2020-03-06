using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace helloLearn.AzDo
{
    public class ResponseWIT
    {
        public int count { get; set; }
        public MyWorkItem[] value { get; set; }        
    }

    public class MyWorkItem
    {
        public int id { get; set; }
        public string url { get; set; }

        public MyField fields { get; set; }
    }

    public class MyField
    {
        [JsonPropertyName("System.Id")]
        public int Id { get; set; }

        [JsonPropertyName("System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonPropertyName("System.Title")]
        public string Title { get; set; }

        [JsonPropertyName("Custom.outline_units")]
        public string outline_Units { get; set; }

        [JsonPropertyName("Custom.TargetReleaseDate")]
        public DateTime TargetReleaseDate { get; set; }

        [JsonPropertyName("Custom.gitPath")]
        public string GitPath { get; set; }

        [JsonPropertyName("Custom.md_ms_author")]
        public string MS_Author { get; set; }

        [JsonPropertyName("Custom.md_ms_date")]
        public DateTime MS_Date { get; set; }

        [JsonPropertyName("Custom.md_ms_prod")]
        public string MS_Product { get; set; }

        [JsonPropertyName("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonPropertyName("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonPropertyName("Custom.TargetRepo")]
        public string Repository { get; set; }

        public string ParseOutlineUnits()
        {
            var inputOutlineString = this.outline_Units;
            string startStr = "id=__md>";
            string endStr = "</div>";
            int startIndex = inputOutlineString.IndexOf(startStr) + startStr.Length;
            int endIndex = inputOutlineString.IndexOf(endStr);
            if (endIndex >= startIndex)
                return inputOutlineString.Substring(startIndex, endIndex - startIndex);

            return inputOutlineString;
        }
    }

}
