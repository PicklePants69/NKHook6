namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;

    [HarmonyPatch(typeof(Simulation), "RemoveCash")]
	class RemoveCashHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		private static readonly int skipNum = 2;
		private static int prefixSkipCount = 0;
		private static int postfixSkipCount = 0;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source)
		{
			//Have to do weird skipping because the event normally fires all the time for no reason
			prefixSkipCount++;
			if (prefixSkipCount < skipNum)
				return true;
			prefixSkipCount = 0;

			bool allowOriginalMethod = true;
			if (sendPrefixEvent)
			{
				var o = new SimulationEvents.RemoveCashEvent.Pre(ref __instance, ref c, ref from, ref cashIndex, ref source);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				c = o.cash;
				from = o.from;
				cashIndex = o.cashIndex;
				source = o.source;
				allowOriginalMethod = !o.isCancelled();
			}
			Logger.Log("RemoveCash Prefix Fired");
			sendPrefixEvent = !sendPrefixEvent;

			return allowOriginalMethod;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source)
		{
			//Have to do weird skipping because the event normally fires all the time for no reason
			postfixSkipCount++;
			if (postfixSkipCount < skipNum)
				return;
			postfixSkipCount = 0;

			if (sendPostfixEvent)
			{
				var o = new SimulationEvents.RemoveCashEvent.Post(ref __instance, ref c, ref from, ref cashIndex, ref source);
				EventRegistry.subscriber.dispatchEvent(ref o);
				__instance = o.instance;
				c = o.cash;
				from = o.from;
				cashIndex = o.cashIndex;
				source = o.source;
			}
			Logger.Log("RemoveCash Postfix Fired");
			sendPostfixEvent = !sendPostfixEvent;
		}
	}

}
