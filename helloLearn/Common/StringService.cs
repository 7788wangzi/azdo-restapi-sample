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

            //Remove special characters
            outputStr = outputStr.Replace(@"/","").Replace(@"\","");
        }

        public string GetLCS(string str1, string str2)
        {
            int lastN = 0;
            string lcs = string.Empty;
            for(int i=0; i<str1.Length; i++)
            {
                for(int j=0; j<str2.Length; j++)
                {
                    if(str1[i]==str2[j])
                    {
                        int n = 1;
                        while ((i+n)<str1.Length&&(j+n)<str2.Length&&(str1[i + n] == str2[j + n]))
                            n++;
                        if (n >= lastN)
                        {
                            lastN = n;
                            //Console.WriteLine("n: "+n+"i: "+i+" ; j: "+j);
                            
                            lcs = str1.Substring(i, n);
                            //Console.WriteLine(lcs);
                        }
                    }
                }
            }

            return lcs;
        }
    }
}
