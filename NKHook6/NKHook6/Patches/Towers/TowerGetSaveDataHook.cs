using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    /// <summary>
    /// Not too sure about this event. I know it fires at the end of each round
    /// </summary>
    [HarmonyPatch(typeof(Tower), "GetSaveData")]
    class TowerGetSaveDataHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerGetSaveDataEvent.Prefix(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref TowerSaveDataModel __result)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerGetSaveDataEvent.Postfix(ref __instance, ref __result);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                __result = o.result;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
