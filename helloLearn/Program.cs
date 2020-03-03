using System;

namespace helloLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Business.ModuleHandler myModule = new Business.ModuleHandler("Manage enterprise security in HDInsight", "Introduction", "Describe HDInsight security areas", "Implement Network security", "Understand operating system security", "Manage application/ middleware security");
            myModule.GenerateModule();
            Console.ReadKey();
        }
    }
}
