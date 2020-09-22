using Il2CppSystem.IO;
using Il2CppSystem.Reflection;
using MelonLoader;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    public class ModSettings : JsonUtils
    {
        const string modsFolder = "\\Mods\\";
        string FileName = "settings.json";

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
