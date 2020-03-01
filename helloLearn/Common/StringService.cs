using System;
using System.Collections.Generic;
using System.Text;

namespace helloLearn.Common
{
    public class StringService
    {
        public void ReplaceSpaceWithDash(string inputStr, out string outputStr)
        {
            outputStr = string.Empty;
            if(!string.IsNullOrEmpty(inputStr))
            {
                var words = inputStr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder newString = new StringBuilder();
                foreach (string word in words)
                {
                    newString.Append(word);
                    newString.Append("-");
                }

                if(newString!=null)
                    outputStr = newString.ToString().Trim('-');
            }

        }

        public void RemoveSmallWords(string inputStr, out string outputStr, List<string> smallwords)
        {
            outputStr = string.Empty;
            var words = inputStr.Split(new string[] { " ","-" }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder newString = new StringBuilder();
            foreach (string word in words)
            {
                if(!smallwords.Contains(word))
                {
                    newString.Append(word);
                    newString.Append("-");
                }
            }

            if (newString != null)
                outputStr = newString.ToString().Trim('-');
        }

        public void ConvertToLowerCase(string inputStr, out string outputStr)
        {
            outputStr = string.Empty;
            if (!string.IsNullOrEmpty(inputStr))
            {
                outputStr = inputStr.Trim().ToLower();
            }
        }
    }
}
