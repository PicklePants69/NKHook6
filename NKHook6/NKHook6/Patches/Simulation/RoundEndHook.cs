namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "OnRoundEnd")]
    class RoundEndHook
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref Simulation __instance, ref int round)
        {
            bool allowOriginalMethod = true;
            var o = new SimulationEvents.RoundEndEvent(ref __instance, ref round);
            EventRegistry.instance.dispatchEvent(ref o);
            round = o.roundArrayIndex;
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }
}
