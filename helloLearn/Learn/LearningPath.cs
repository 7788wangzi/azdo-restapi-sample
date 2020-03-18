using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace helloLearn.Learn
{
    public class LearningPath:IWorkItem
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Module> Modules { get; set; }        
        public string TargetFolder { get; set; }
        public string UID { get; set; }
        public string Url { get; set; }
        public string WorkItemType { get; set; }
        public DateTime MsDate { get; set; }
        public string MsAuthor { get; set; }
        public string MsProduct { get; set; }
        public string AreaPath { get; set; }
        public string IteractionPath { get; set; }
        public string OutlineStructure { get; set; }
        public string GitPath { get; set; }
    }
}
