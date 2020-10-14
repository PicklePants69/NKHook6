using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Gamemodes
{
    public class Loader
    {
        public static List<Gamemode> customGameModes = new List<Gamemode>();

        public static void Start()
        {
            Logger.Log("There are currently " + customGameModes.Count + " custom gamemodes");

            if (customGameModes.Count > 0)
                customGameModes[0].IsLoaded = true;
        }
    }
}
