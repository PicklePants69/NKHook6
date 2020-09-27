namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "TakeDamage")]
	class TakeDamageHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref float damage)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new SimulationEvents.TakeDamageEvent.Pre(ref __instance, ref damage);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				damage = o.damage;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Simulation __instance, ref float damage)
		{
			if (sendPostfixEvent)
			{
				var o = new SimulationEvents.TakeDamageEvent.Post(ref __instance, ref damage);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				damage = o.damage;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
