using System;
using System.Collections.Generic;
using System.Text;
using helloLearn.Learn;

namespace helloLearn.Business
{
    public class LearningPathHandler
    {
        public LearningPath ThisLP { get; set; }
        public LearningPathHandler()
        {
            ThisLP = new LearningPath();
        }

        public LearningPathHandler(int id, string lpUrl, string lpTitle, params string[] moduleTitles)
        {
            FileHandler myFileHandler = new FileHandler();
            ThisLP = new LearningPath();
            ThisLP.Id = id;
            ThisLP.Title = lpTitle;
            ThisLP.Url = lpUrl;
            ThisLP.TargetFolder = myFileHandler.ProcessFileNames(lpTitle);
            ThisLP.Modules = new List<Module>();
            ThisLP.UID = "learn.wwl." + ThisLP.UID;
            foreach (var module in moduleTitles)
            {

                ThisLP.Modules.Add(new Module()
                {
                    Title = module,
                    ModuleFolder = myFileHandler.ProcessFileNames(module)

                });
            }
        }

        public LearningPath GenerateLearningPath()
        {
            IOCls myIO = new IOCls();
            var jsonMessage = myIO.SerializeLP(ThisLP);
            myIO.WriteLog(string.Format(@"LP-{0}-log.json",ThisLP.Title), jsonMessage);

            return ThisLP;
        }
    }
}
