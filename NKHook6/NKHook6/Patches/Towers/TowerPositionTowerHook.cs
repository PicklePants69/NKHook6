using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "PositionTower")]
    class TowerPositionTowerHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Vector2 newPosition, ref bool updateThrowCache)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerPositionTowerEvent.Prefix(ref __instance, ref newPosition, ref updateThrowCache);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                newPosition = o.newPosition;
                updateThrowCache = o.updateThrowCache;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Vector2 newPosition, ref bool updateThrowCache)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerPositionTowerEvent.Postfix(ref __instance, ref newPosition, ref updateThrowCache);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.tower;
                newPosition = o.newPosition;
                updateThrowCache = o.updateThrowCache;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
