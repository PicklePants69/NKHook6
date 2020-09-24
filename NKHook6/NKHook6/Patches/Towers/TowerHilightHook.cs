
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;
using NKHook6.Api.Utilities;

namespace NKHook6.Patches.Towers
{
	[HarmonyPatch(typeof(Tower), "Hilight")]
	class TowerHilightHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Tower __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new TowerHilightEvent.Prefix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.tower;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Tower __instance)
		{
			if (sendPostfixEvent)
			{
				var o = new TowerHilightEvent.Postfix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.tower;
			}
			
			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
