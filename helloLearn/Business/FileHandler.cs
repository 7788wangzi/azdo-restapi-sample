using helloLearn.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.Business
{
    public class FileHandler
    {
        public string ProcessFileNames(string fileName)
        {
            string result = string.Empty;
            IOCls ioCLS = new IOCls();
            var config = ioCLS.GetConfiguration();
            StringService strService = new StringService();

            //Lower case the file name
            strService.ConvertToLowerCase(fileName, out result);
            //Remove invalid characters for filename
            strService.ReplaceInvalidCharactersInFileName(result, out result);
            //Replace space with dash
            strService.ReplaceSpaceWithDash(result, out result);
            //Remove small words
            if (config.SmallWords != null)
                strService.RemoveSmallWords(result, out result, config.SmallWords);
            //Make sure file name doesn't exceed max length
            if (config.FileMaxLength != 0 && result.Length > config.FileMaxLength)
                result = result.Substring(0, config.FileMaxLength);

            return result;
        }

        public string RemoveRedundancyFromPath(string moduleFolder, string fileName)
        {
            StringService strService = new StringService();
            var redundancyString = strService.GetLCSWords(moduleFolder, fileName);
            var words = redundancyString.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            //If there are two more words redundancy, then remove from file name
            if(words.Length>=2)
                return fileName.Replace(redundancyString, "").Trim('-');
            return fileName;
        }
    }
}
