using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.Learn
{
    public class Module
    {
        public string Title { get; set; }
        public string UID { get; set; }
        public List<Unit> Units { get; set; }
        public string ModuleFolder { get; set; }
    }
}
