namespace NKHook6.Patches._Projectile
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers.Projectiles;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Projectile;

    [HarmonyPatch(typeof(Projectile), "Initialise")]
	class InitialiseHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Projectile __instance, ref Entity target, ref Model modelToUse)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new ProjectileEvents.InitialiseEvent.Pre(ref __instance, ref target, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				target = o.entity;
				modelToUse = o.model;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Projectile __instance, ref Entity target, ref Model modelToUse)
		{
			if (sendPostfixEvent)
			{
				var o = new ProjectileEvents.InitialiseEvent.Post(ref __instance, ref target, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				target = o.entity;
				modelToUse = o.model;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
