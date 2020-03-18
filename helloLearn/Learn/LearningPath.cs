using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace helloLearn.Learn
{
    public class LearningPath
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Module> Modules { get; set; }        
        public string TargetFolder { get; set; }
        public string UID { get; set; }
        public string Url { get; set; }
    }
}
