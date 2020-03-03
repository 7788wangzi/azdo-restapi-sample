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
    }
}
