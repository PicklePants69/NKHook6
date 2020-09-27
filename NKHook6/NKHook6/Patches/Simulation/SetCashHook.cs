namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;
    using System.Runtime.InteropServices;

    [HarmonyPatch(typeof(Simulation), "SetCash")]
	class SetCashHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref double c, [Optional]ref int cashIndex)
		{
			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new SetCashEvent.Pre(ref __instance, ref c, ref cashIndex);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				c = o.cash;
				cashIndex = o.cashIndex;
				allowOriginalMethod = !o.isCancelled();
			}
			
			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Simulation __instance, ref double c, [Optional]ref int cashIndex)
		{
			if (sendPostfixEvent)
			{
				var o = new SetCashEvent.Post(ref __instance, ref c, ref cashIndex);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				c = o.cash;
				cashIndex = o.cashIndex;
			}

			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
