namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    [HarmonyPatch(typeof(Tower), "AddPoppedCash")]
    class AddPoppedCashHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref float cash)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerEvents.AddPoppedCashEvent.Pre(ref __instance, ref cash);
                EventRegistry.instance.dispatchEvent(ref o);
                __instance = o.instance;
                cash = o.cash;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref float cash)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerEvents.AddPoppedCashEvent.Post(ref __instance, ref cash); ;
                EventRegistry.instance.dispatchEvent(ref o);
                __instance = o.instance;
                cash = o.cash;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
