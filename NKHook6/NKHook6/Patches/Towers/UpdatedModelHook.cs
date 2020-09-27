using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "UpdatedModel")]
    class UpdatedModelHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new UpdatedModelEvent.Pre(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                modelToUse = o.model;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new UpdatedModelEvent.Post(ref __instance, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
