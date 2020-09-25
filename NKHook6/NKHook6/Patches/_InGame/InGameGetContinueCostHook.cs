using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Patches.__InGame
{
	using Assets.Scripts.Unity.UI_New.InGame;
	using Assets.Scripts.Utils;
	using Harmony;
	using NKHook6.Api.Events;
	using NKHook6.Api.Events.__InGame;

	[HarmonyPatch(typeof(InGame), "GetContinueCost")]
	class InGameGetContinueCostHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref InGame __instance)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new InGameGetContinueCostEvent.Prefix(ref __instance);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.inGame;
				allowOriginalMethod = !o.isCancelled();
			}

			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref InGame __instance, ref KonFuze __result)
		{
			if (sendPostfixEvent)
			{
				var o = new InGameGetContinueCostEvent.Postfix(ref __instance, ref __result);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.inGame;
				__result = o.konFuze;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
