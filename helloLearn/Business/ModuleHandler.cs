using System;
using System.Collections.Generic;
using System.Text;
using helloLearn.Learn;
using helloLearn.Business;
using System.Text.RegularExpressions;

namespace helloLearn.Business
{
    public class ModuleHandler
    {
        public Module ThisModule { get; set; }
        public ModuleHandler()
        {
            ThisModule = new Module();
        }

        public ModuleHandler(int id, string url, string repository, string title, params string[] unitTitles)
        {
            ThisModule = new Module();
            ThisModule.Id = id;
            ThisModule.Title = title;
            ThisModule.Url = url;
            ThisModule.Repository = repository;

            //module with prefix            
            var match = Regex.Match(title.Trim(), "^\\d{1,}[ ]{0,}-");
            int prefix = 0;
            if (Int32.TryParse(match.Value.Trim('-').Trim(), out prefix))
            {
                ThisModule.Prefix = prefix;
                ThisModule.Title = title.Replace(match.Value, "");
            }


            ThisModule.Units = new List<Unit>();
            for(int i=0; i<unitTitles.Length; i++)
            {
                ThisModule.Units.Add(
                    new Unit { Title = unitTitles[i].Trim() });
            }
        }

        public Module GenerateModule()
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

                        //Remove reduandency from the path ThisModule.ModuleFolder & fileName
                        fileName = myFileHandler.RemoveRedundancyFromPath(ThisModule.ModuleFolder, fileName);

                        unit.Title = prefix + "-" + unit.Title;
                        unit.FileName = prefix + "-" + fileName;
                        unit.UID = fileName;
                        prefix++;
                    }
                }

                //Create files
                IOCls myIO = new IOCls();
                string folderName = ThisModule.ModuleFolder;
                //Add prefix to module folder
                if (ThisModule.Prefix != 0)
                {
                    folderName = "M" + ThisModule.Prefix.ToString("D2") + "-" + ThisModule.ModuleFolder;
                }
                var folderDir = myIO.CreateFolder(folderName: folderName);
                //Create includes folder
                var targetPath = myIO.CreateFolder(folderDir.FullName, "includes");
                foreach (var unit in ThisModule.Units)
                {                    
                    myIO.CreateNewFile(targetPath.FullName, unit.FileName);
                }

                var jsonMessage = myIO.SerializeModule(ThisModule);
                myIO.WriteLog(folderDir + "\\log.json", jsonMessage);

                //Create media folder
                var mediaPath = myIO.CreateFolder(folderDir.FullName, "media");
                myIO.CreateNewFile(mediaPath.FullName, "placeholder");

                //Create badge folder
                var badgePath = myIO.CreateFolder(folderDir.FullName, "badge");
                myIO.CreateNewFile(badgePath.FullName, "placeholder");

                //Create Compliance_Results folder
                var complianceFolder = myIO.CreateFolder(folderDir.FullName, "Compliance_Results");
                myIO.CreateNewFile(complianceFolder.FullName, "placeholder");

                //Create Video_CC folder
                var videoCCFolder = myIO.CreateFolder(folderDir.FullName, "Video_CC");
                myIO.CreateNewFile(videoCCFolder.FullName, "placeholder");

                //Create metadata file
                var metadataString = myIO.GetMetadataString();
                metadataString = String.Format(metadataString, ThisModule.Id, ThisModule.ModuleFolder, ThisModule.Repository);
                string metadataFileName = "Metadata.md";

                //Add prefix to module folder
                if (ThisModule.Prefix != 0)
                {
                    metadataFileName = "M" + ThisModule.Prefix.ToString("D2") + " " + metadataFileName;
                }
                myIO.WriteLog(folderDir + $"\\{metadataFileName}", metadataString);

                return ThisModule;
            }

            return null;
        }
    }
}
