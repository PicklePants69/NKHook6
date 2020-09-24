using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "UpdatedModel")]
    class TowerUpdatedModelHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerUpdatedModelEvent.Prefix(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                modelToUse = o.model;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerUpdatedModelEvent.Postfix(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
