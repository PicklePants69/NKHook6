namespace NKHook6.Patches._Weapons
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Weapons;

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
				__instance = o.instance;
				target = o.entity;
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
				__instance = o.instance;
				target = o.entity;
				modelToUse = o.model;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
