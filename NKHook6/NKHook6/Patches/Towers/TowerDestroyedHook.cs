using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "OnDestroy")]
    class TowerDestroyedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerDestroyedEvent.Prefix(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerDestroyedEvent.Postfix(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
