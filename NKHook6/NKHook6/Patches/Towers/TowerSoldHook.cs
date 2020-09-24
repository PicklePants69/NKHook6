using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "OnSold")]
    class OnSoldHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref float amount)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerSoldEvent.Prefix(ref __instance, ref amount);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                amount = o.sellAmount;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref float amount)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerSoldEvent.Postfix(ref __instance, ref amount); ;
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                amount = o.sellAmount;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
