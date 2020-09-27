namespace NKHook6.Patches._InGame
{
    using Assets.Scripts.Unity.UI_New.InGame;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._InGame;

    [HarmonyPatch(typeof(InGame), "OnVictory")]
	class OnVictoryHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref InGame __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new OnVictoryEvent.Pre(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref InGame __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new OnVictoryEvent.Post(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
