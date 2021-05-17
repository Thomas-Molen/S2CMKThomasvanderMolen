using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseConntectionTesting
{
    public class SaveDataHelper
    {
        private string appdataDirectory;
        private string GameFolder;
        private string SaveFile;

        public SaveDataHelper()
        {
            appdataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            GameFolder = Path.Combine(appdataDirectory, "platformerspeedrunner-thomasvandermolen");
            SaveFile = Path.Combine(GameFolder, "UniqueKey.txt");
        }

        public void CreateSaveFile()
        {
            //create appdata folder if does not exist
            Directory.CreateDirectory(GameFolder);

            //create textfile if does not exist or is empty
            if (!File.Exists(SaveFile) || File.ReadAllText(SaveFile) == "")
            {
                HttpConnector connector = new HttpConnector();
                File.WriteAllText(SaveFile, connector.GetUniqueKey());
            }
        }

        public string GetSaveData()
        {
            return File.ReadAllText(SaveFile);
        }
    }
}
