namespace NKHook6.Patches.Weapons
{
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events.Weapons;

    [HarmonyPatch(typeof(Weapon), "OnDestroy")]
	class TowerOnDestroyHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Weapon __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new WeaponOnDestroyEvent.Prefix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weapon;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Weapon __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new WeaponOnDestroyEvent.Postfix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weapon;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
