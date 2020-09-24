/*namespace NKHook6.Patches.WeaponBehaviors
{
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events.WeaponBehaviors;

    [HarmonyPatch(typeof(WeaponBehavior), "CanFire")]
	class WeaponBehaviorCanFireHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref WeaponBehavior __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new WeaponBehaviorCanFireEvent.Prefix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weaponBehavior;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref WeaponBehavior __instance, ref bool __result)
		{
			if (sendPostfixEvent)
			{
				var o = new WeaponBehaviorCanFireEvent.Postfix(ref __instance, ref __result);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weaponBehavior;
				__result = o.result;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}
}
*/