namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "OnRoundStart")]
    class RoundStartHook
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref Simulation __instance, ref int roundArrayIndex)
        {
            bool allowOriginalMethod = true;
            var o = new SimulationEvents.RoundStartEvent(ref __instance, ref roundArrayIndex);
            EventRegistry.subscriber.dispatchEvent(ref o);
            roundArrayIndex = o.roundArrayIndex;
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }
}
