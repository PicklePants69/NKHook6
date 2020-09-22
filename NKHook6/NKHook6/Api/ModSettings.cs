using Il2CppSystem.IO;
using Il2CppSystem.Reflection;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    public class ModSettings : JsonUtils
    {
        string modsFolder = "Mods/";

        public ModSettings()
        {

        }

        public ModSettings(string filePath)
        {
            //FilePath = modsFolder + "\\" + modName;
            
            //FilePath = Environment.CurrentDirectory + "\\Mods\\" + Assembly.GetCallingAssembly().FullName;
        }
    }
}
