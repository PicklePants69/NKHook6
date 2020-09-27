namespace NKHook6.Patches._InGame
{
	using Assets.Scripts.Unity.UI_New.InGame;
	using Assets.Scripts.Utils;
	using Harmony;
	using NKHook6.Api.Events;
	using NKHook6.Api.Events._InGame;

	[HarmonyPatch(typeof(InGame), "GetContinueCost")]
	class GetContinueCostHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref InGame __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new InGameEvents.GetContinueCostEvent.Pre(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref InGame __instance, ref KonFuze __result)
		{
			if (sendPostfixEvent)
			{
				var o = new InGameEvents.GetContinueCostEvent.Post(ref __instance, ref __result);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				__result = o.konFuze;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
