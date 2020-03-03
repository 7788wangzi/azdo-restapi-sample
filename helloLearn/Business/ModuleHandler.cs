using System;
using System.Collections.Generic;
using System.Text;
using helloLearn.Learn;
using helloLearn.Business;

namespace helloLearn.Business
{
    public class ModuleHandler
    {
        public Module ThisModule { get; set; }
        public ModuleHandler()
        {
            ThisModule = new Module();
        }

        public ModuleHandler(string title, params string[] unitTitles)
        {
            ThisModule = new Module();
            ThisModule.Title = title;

            ThisModule.Units = new List<Unit>();
            for(int i=0; i<unitTitles.Length; i++)
            {
                ThisModule.Units.Add(
                    new Unit { Title = unitTitles[i] });
            }
        }

        public void GenerateModule()
        {
            if(ThisModule!=null&&ThisModule.Title!=null)
            {
                //Generate UIDs and folder names
                FileHandler myFileHandler = new FileHandler();
                ThisModule.ModuleFolder = myFileHandler.ProcessFileNames(ThisModule.Title);
                ThisModule.UID = "learn.wwl." + ThisModule.ModuleFolder;

                if(ThisModule.Units!=null)
                {
                    int prefix = 1;
                    foreach (var unit in ThisModule.Units)
                    {
                        var fileName = myFileHandler.ProcessFileNames(unit.Title);
                        unit.FileName = prefix + "-" + fileName;
                        unit.UID = fileName;
                        prefix++;
                    }
                }

                //Create files
                StringBuilder moduleTOC = new StringBuilder();
                moduleTOC.AppendLine("Module UID: " + ThisModule.UID);
                moduleTOC.AppendLine("Units: ");
                IOCls myIO = new IOCls();
                var folderDir = myIO.CreateFolder(folderName: ThisModule.ModuleFolder);
                var targetPath = myIO.CreateFolder(folderDir.FullName, "includes");
                foreach (var unit in ThisModule.Units)
                {                    
                    myIO.CreateNewFile(targetPath.FullName, unit.FileName);
                    moduleTOC.AppendLine(unit.UID);
                }

                myIO.WriteLog(folderDir + "\\log.txt", moduleTOC.ToString());
            }
        }
    }
}
