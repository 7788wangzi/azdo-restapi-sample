﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using helloLearn.Common;

namespace helloLearn.Business
{
    public class IOCls
    {
        public Config GetConfiguration()
        {
            using(StreamReader reader = new StreamReader("./Config/config.json"))
            {
                var json = reader.ReadToEnd();

                var MyConfig = JsonSerializer.Deserialize<Config>(json);
                if (MyConfig != null)
                    return MyConfig;
            }

            return null;
        }

        public void CreateNewFile(string path, string fileName)
        {
            string fileFullPath = Path.Combine(path, fileName + ".md");
            File.Create(fileFullPath).Close();
        }

        public DirectoryInfo CreateFolder(string path="", string folderName="")
        {
            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();
            string folderFullPath = Path.Combine(path, folderName);

            //If already exist, then delete first
            if (Directory.Exists(folderFullPath))
                Directory.Delete(folderFullPath, true);
            return Directory.CreateDirectory(folderFullPath);
        }

        public void WriteLog(string fileName, string message)
        {
            using(StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(message);
                writer.Flush();
            }
        }
    }
}
