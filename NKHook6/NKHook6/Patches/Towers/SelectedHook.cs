namespace NKHook6.Patches._Towers
{
	using Assets.Scripts.Simulation.Towers;
	using Harmony;
	using NKHook6.Api.Events;
	using NKHook6.Api.Events._Towers;

	[HarmonyPatch(typeof(Tower), "Hilight")]
	class SelectedHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Tower __instance)
		{
			bool allowOriginalMethod = true;
			var o = new TowerEvents.SelectedEvent(ref __instance);
			EventRegistry.subscriber.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;
		}
	}
}
