using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "GetUpgrade")]
    class TowerGetUpgradeHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref int path)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerGetUpgradeEvent.Prefix(ref __instance, ref path);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                path = o.path;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref int path, ref UpgradeModel __result)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerGetUpgradeEvent.Postfix(ref __instance, ref path, ref __result);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                path = o.path;
                __result = o.result;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
