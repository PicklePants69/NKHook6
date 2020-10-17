using Assets.Scripts.Unity.Player;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Patches.Bloon6Player
{
    [HarmonyPatch(typeof(Btd6Player), "GetAnalyticsInfo")]
    class Bloon6PlayerPatch
    {
        internal static Btd6Player thePlayer;
        [HarmonyPostfix]
        internal static void Postfix(Btd6Player __instance)
        {
            thePlayer = __instance;
        }
    }
}
