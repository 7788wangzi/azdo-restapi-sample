using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.Learn
{
    public interface IWorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string WorkItemType { get; set; }
        public DateTime MsDate { get; set; }
        public string MsAuthor { get; set; }
        public string MsProduct { get; set; }
        public string AreaPath { get; set; }
        public string IteractionPath { get; set; }
        public string OutlineStructure { get; set; }
        public string GitPath { get; set; }
    }

    public enum WorkItemType
    {
        
        LearningPath = 1,
        Module,
        Unit

    }
}
