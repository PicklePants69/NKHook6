namespace NKHook6.Patches._Projectile
{
    using Assets.Scripts.Simulation.Towers.Projectiles;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Projectile;

    [HarmonyPatch(typeof(Projectile), "OnDestroy")]
	class OnDestroyHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Projectile __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new ProjectileEvents.OnDestroyEvent.Pre(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Projectile __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new ProjectileEvents.OnDestroyEvent.Post(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
