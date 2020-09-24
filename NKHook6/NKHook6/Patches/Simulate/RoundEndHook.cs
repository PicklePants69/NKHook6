using Assets.Scripts.Simulation;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Simulate;

namespace NKHook6.Patches.Simulate
{
    [HarmonyPatch(typeof(Simulation), "OnRoundEnd")]
    class RoundEndHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Simulation __instance, ref int round)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new RoundEndEvent.Prefix(ref __instance, ref round);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.round;
                round = o.roundArrayIndex;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Simulation __instance, ref int round)
        {
            if (sendPostfixEvent)
            {
                var o = new RoundEndEvent.Postfix(ref __instance, ref round);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.round;
                round = o.roundArrayIndex;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
