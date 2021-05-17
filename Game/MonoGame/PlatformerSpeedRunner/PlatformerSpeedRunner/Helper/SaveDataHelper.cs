using System;
using System.IO;

namespace PlatformerSpeedRunner.Helper
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

        public void CheckSaveFile()
        {
            //create appdata folder if does not exist
            Directory.CreateDirectory(GameFolder);

            //create textfile if does not exist or is empty
            if (!File.Exists(SaveFile) || File.ReadAllText(SaveFile) == "")
            {
                DatabaseHelper connector = new DatabaseHelper();
                File.WriteAllText(SaveFile, connector.GetUniqueKey());
            }
        }

        public string GetSaveData()
        {
            CheckSaveFile();
            return File.ReadAllText(SaveFile);
        }
    }
}
