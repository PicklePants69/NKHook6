namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;
    using System.Runtime.InteropServices;

    [HarmonyPatch(typeof(Simulation), "AddCash")]
	class CashGainedHook
	{
		private static bool sendPrefixEvent = true;
		private static bool sendPostfixEvent = true;

		private static readonly int skipNum = 2;
		private static int prefixSkipCount = 0;
		private static int postfixSkipCount = 0;

		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source, [Optional] ref Tower tower)
		{
			double cash = c;
			//Have to do weird skipping because the event normally fires all the time for no reason
			prefixSkipCount++;
			if (cash == 0 || (prefixSkipCount < skipNum))
				return true;
			prefixSkipCount = 0;

			bool allowOriginalMethod = true;
			var p = new SimulationEvents.CashChangedEvent(__instance, cash, from, cashIndex, source, tower);
			EventRegistry.subscriber.dispatchEvent(ref p);
			if (cash > 0)
			{
				var o = new SimulationEvents.CashGainedEvent(__instance, cash, from, cashIndex, source, tower);
				EventRegistry.subscriber.dispatchEvent(ref o);

				cash = o.cash;
				from = o.from;
				cashIndex = o.cashIndex;
				source = o.source;
				tower = o.tower;
				allowOriginalMethod = !(o.isCancelled() || p.isCancelled());

				return allowOriginalMethod;
			}
            else
            {
				var o = new SimulationEvents.CashLostEvent(__instance, cash, from, cashIndex, source);
				EventRegistry.subscriber.dispatchEvent(ref o);

				cash = o.cash;
				from = o.from;
				cashIndex = o.cashIndex;
				source = o.source;
				allowOriginalMethod = !(o.isCancelled() || p.isCancelled());

				return allowOriginalMethod;
			}
		}
	}
}
