namespace NKHook6.Patches.Simulate
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "OnDefeat")]
	class OnDefeatHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new OnDefeatEvent.Prefix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.simulation;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Simulation __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new OnDefeatEvent.Postfix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.simulation;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
