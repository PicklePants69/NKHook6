namespace NKHook6.Patches._Towers
{
	using Assets.Scripts.Simulation.Towers;
	using Harmony;
	using NKHook6.Api.Events;
	using NKHook6.Api.Events._Towers;

	[HarmonyPatch(typeof(Tower), "UnHighlight")]
	class UnHighlightHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Tower __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new UnHighlightEvent.Pre(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Tower __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new UnHighlightEvent.Post(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
