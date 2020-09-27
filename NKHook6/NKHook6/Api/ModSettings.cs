using Il2CppSystem.IO;
using NKHook6.Api.Utilities;
using System;

namespace NKHook6.Api
{
    public class ModSettings
    {
        const string modsFolder = "\\Mods\\";
        string FileName = "settings.json";
        string FilePath = "";
        public ModSettings()
        {
            string modName = Utils.GetCallingModInfo().Name;

            if (!String.IsNullOrEmpty(modName))
            {
                FilePath = Environment.CurrentDirectory + modsFolder + modName;

                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);

                FilePath += "\\" + FileName;
            }
        }

        public ModSettings(string fileName)
        {
            if (fileName.StartsWith("\\") || fileName.StartsWith("/"))
                fileName = fileName.TrimStart('\\').TrimStart('/');

            FileName = fileName;
        }


    }
}
