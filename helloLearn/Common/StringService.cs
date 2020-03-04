using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

        public void ReplaceInvalidCharactersInFileName(string inputStr, out string outputStr)
        {
            outputStr = string.Empty;
            if(!string.IsNullOrEmpty(inputStr))
            {
                outputStr=Regex.Replace(inputStr, "[\\\\/:*?\"<>|]", "");
            }
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


        public string GetLCSWords(string str1, string str2)
        {
            var words1 = str1.ToLower().Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries);
            var words2 = str2.ToLower().Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries);

            int lastN = 0;
            StringBuilder lcsWord = new StringBuilder();
            for (int i = 0; i < words1.Length; i++)
            {
                for (int j = 0; j < words2.Length; j++)
                {
                    if (words1[i] == words2[j])
                    {
                        int n = 1;
                        while ((i + n) < words1.Length && (j + n) < words2.Length && (words1[i + n] == words2[j + n]))
                            n++;
                        if (n >= lastN)
                        {
                            lcsWord = new StringBuilder();
                            lastN = n;
                            int s = i;
                            int c = 0;
                            while (c < n)
                            {
                                lcsWord.Append(words1[s]);
                                lcsWord.Append("-");
                                c++;
                                s += c;
                            }
                        }
                    }
                }
            }
            return lcsWord.ToString().TrimEnd('-');
        }


    }
}
