using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.Learn
{
    public class Module
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UID { get; set; }
        public List<Unit> Units { get; set; }
        public string ModuleFolder { get; set; }
        public string Url { get; set; }
        public int Prefix { get; set; }
        public string Repository { get; set; }
    }
}
