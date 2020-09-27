namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

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
                var o = new RoundEndEvent.Pre(ref __instance, ref round);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
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
                var o = new RoundEndEvent.Post(ref __instance, ref round);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                round = o.roundArrayIndex;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
