namespace NKHook6.Patches._InGame
{
	using Assets.Scripts.Unity.UI_New.InGame;
	using Harmony;
	using NKHook6.Api.Events;
	using NKHook6.Api.Events._InGame;

	[HarmonyPatch(typeof(InGame), "StartMatch")]
	class StartMatchHook
	{
		[HarmonyPostfix]
		internal static bool Postfix(ref InGame __instance)
		{
			bool allowOriginalMethod = true;
			var o = new InGameEvents.StartMatchEvent(ref __instance);
			EventRegistry.instance.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();
			return allowOriginalMethod;
		}
	}
}
