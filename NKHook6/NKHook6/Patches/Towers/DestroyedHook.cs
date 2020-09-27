namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events.Towers;

    [HarmonyPatch(typeof(Tower), "OnDestroy")]
    class DestroyedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new DestroyedEvent.Pre(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance)
        {
            if (sendPostfixEvent)
            {
                var o = new DestroyedEvent.Post(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
