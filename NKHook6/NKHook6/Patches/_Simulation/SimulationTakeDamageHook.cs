namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "TakeDamage")]
	class SimulationTakeDamageHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref float damage)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new SimulationTakeDamageEvent.Prefix(ref __instance, ref damage);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.simulation;
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
				var o = new SimulationTakeDamageEvent.Postfix(ref __instance, ref damage);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.simulation;
				damage = o.damage;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
