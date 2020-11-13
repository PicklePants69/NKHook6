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
		internal static void Postfix(ref InGame __instance, ref bool isFromSave)
		{
			Logger.Log("Event postfix");
			var o = new InGameEvents.StartMatchEvent(ref __instance, ref isFromSave);
			EventRegistry.instance.dispatchEvent(ref o);
			isFromSave = o.isFromSave;
		}
	}
}
