using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "AddPoppedCash")]
    class TowerAddPoppedCashHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref float cash)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerAddPoppedCashEvent.Prefix(ref __instance, ref cash);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                cash = o.cash;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref float cash)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerAddPoppedCashEvent.Postfix(ref __instance, ref cash); ;
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                cash = o.cash;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
