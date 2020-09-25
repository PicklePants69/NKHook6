namespace NKHook6.Patches.Weapons
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events.Weapons;

    [HarmonyPatch(typeof(Weapon), "Initialise")]
	class InitialiseHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Weapon __instance, ref Entity target, ref Model modelToUse)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new InitialiseEvent.Pre(ref __instance, ref target, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weapon;
				target = o.target;
				modelToUse = o.model;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Weapon __instance, ref Entity target, ref Model modelToUse)
		{
			if (sendPostfixEvent)
			{
				var o = new InitialiseEvent.Post(ref __instance, ref target, ref modelToUse);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.weapon;
				target = o.target;
				modelToUse = o.model;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
