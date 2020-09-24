using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity.Towers.Mods;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Bloons;

namespace NKHook6.Patches.Bloons
{
    [HarmonyPatch(typeof(Bloon), "UpdatedModel")]
    class BloonUpdatedModelHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new BloonUpdatedModelEvent.Prefix(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                modelToUse = o.model;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new BloonUpdatedModelEvent.Postfix(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
