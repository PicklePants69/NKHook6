namespace NKHook6.Patches._Weapons
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Weapons;

    [HarmonyPatch(typeof(Weapon), "UpdatedModel")]
	class UpdatedModelHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Weapon __instance, ref Model modelToUse)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new WeaponEvents.UpdatedModelEvent.Pre(ref __instance, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				modelToUse = o.model;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Weapon __instance, ref Model modelToUse)
		{
			if (sendPostfixEvent)
			{
				var o = new WeaponEvents.UpdatedModelEvent.Post(ref __instance, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				modelToUse = o.model;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
