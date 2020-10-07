namespace NKHook6.Patches._InGame
{
    using Assets.Scripts.Unity.UI_New.InGame;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._InGame;

    [HarmonyPatch(typeof(InGame), "OnVictory")]
	class VictoryHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref InGame __instance)
		{
			bool allowOriginalMethod = true;
			var o = new InGameEvents.VictoryEvent(ref __instance);
			EventRegistry.subscriber.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();
			return allowOriginalMethod;
		}
	}
}
