using System;
using System.IO;
using helloLearn.AzDo;
using System.Text.Json;
using System.Collections.Generic;

namespace helloLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Business.ModuleHandler myModule = new Business.ModuleHandler("Manage enterprise security in HDInsight", "Introduction", "Describe HDInsight security areas", "Implement Network security", "Understand operating system security", "Manage application/ middleware security", "Enterprise security further explore");
            //myModule.GenerateModule();

            Console.WriteLine("Program started...");
            //using (StreamReader reader = new StreamReader("input.txt"))
            //{
            //    string content = reader.ReadToEnd();
            //    Console.WriteLine("Read titles from input.txt successfully...");
            //    var parameters = content.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
            //    string[] unitParameters = new string[parameters.Length-1];
            //    for(int i=1; i<parameters.Length; i++)
            //    {
            //        unitParameters[i - 1] = parameters[i];
            //    }
            //    Console.WriteLine("Parse titles successfully...");
            //    try
            //    {
            //        Business.ModuleHandler myModule = new Business.ModuleHandler(parameters[0], unitParameters);
            //        myModule.GenerateModule();
            //        Console.WriteLine("Generate module TOC successfully.");
            //    }
            //    catch(Exception ex)
            //    {
            //        Console.WriteLine("Error:");
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            //Console.WriteLine("Press any key to exit.");

            //int workItemId = 9089; //module

            //int workItemId = 9285; //LP
            int workItemId = 1;

            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("Read titles from input.txt successfully...");
                var parameters = Int32.Parse(content.Trim());
                workItemId = parameters;
                
            }
            //Console.WriteLine("Press any key to exit.");


            Console.WriteLine("Process work item: "+ workItemId);
            System.Threading.Thread.Sleep(3000);
            AzDo.WorkItemHandler workItemHandler = new AzDo.WorkItemHandler();
            workItemHandler.ProcessWorkItem(workItemId);

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Tasks all done!");
            Console.ReadKey();
        }
    }
}
