using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.AzDo
{
    public class JsonPatchDocument
    {
        public JsonPatch[] body { get; set; }
    }

    public class JsonPatch
    {
        public string op { get; set; }
        public string path { get; set; }

        public Object value { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string url { get; set; }
        public Attribute attributes { get; set; }
    }

    public class Attribute
    {
        public string comment { get; set; }
    }
}
