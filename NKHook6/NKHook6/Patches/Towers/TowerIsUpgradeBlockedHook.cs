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
    [HarmonyPatch(typeof(Tower), "IsUpgradeBlocked")]
    class TowerIsUpgradeBlockedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref int path, ref int tier)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerIsUpgradeBlockedEvent.Prefix(ref __instance, ref path, ref tier);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
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
                var o = new TowerIsUpgradeBlockedEvent.Postfix(ref __instance, ref path, ref tier, ref reason, ref __result);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                path = o.path;
                tier = o.tier;
                reason = o.reason;
                __result = o.result;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
