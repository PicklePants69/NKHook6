namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    /// <summary>
    /// Not too sure about this event. I know it fires at the end of each round
    /// </summary>
    [HarmonyPatch(typeof(Tower), "IsUpgradeBlocked")]
    class IsUpgradeBlockedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref int path, ref int tier)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new UpgradeBlockedEvent.Pre(ref __instance, ref path, ref tier);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                path = o.path;
                tier = o.tier;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref int path, ref int tier, ref string reason, ref bool __result)
        {
            if (sendPostfixEvent)
            {
                var o = new UpgradeBlockedEvent.Post(ref __instance, ref path, ref tier, ref reason, ref __result);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                path = o.path;
                tier = o.tier;
                reason = o.reason;
                __result = o.result;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
