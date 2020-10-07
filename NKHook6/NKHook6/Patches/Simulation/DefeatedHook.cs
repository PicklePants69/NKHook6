namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "OnDefeat")]
	class DefeatedHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance)
		{
			bool allowOriginalMethod = true;
			var o = new SimulationEvents.DefeatedEvent(ref __instance);
			EventRegistry.subscriber.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;
		}
	}
}
