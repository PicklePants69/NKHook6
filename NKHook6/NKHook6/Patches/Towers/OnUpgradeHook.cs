

using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
	[HarmonyPatch(typeof(Tower), "OnUpgrade")]
	class OnUpgradeHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Tower __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new OnUpgradeEvent.Pre(ref __instance);
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
				var o = new OnUpgradeEvent.Post(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.tower;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
