namespace NKHook6.Patches._Simulation
{
    using Assets.Scripts.Simulation;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Simulation;
    using System.Runtime.InteropServices;
    using static Assets.Scripts.Simulation.Simulation;

    [HarmonyPatch(typeof(Simulation), "SetCash")]
	class SetCashHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Simulation __instance, ref double c, [Optional] ref int cashIndex)
		{
			/*bool allowOriginalMethod = true;
			var o = new SimulationEvents.SetCashEvent.Pre(__instance, cash, cashIndex);
			EventRegistry.subscriber.dispatchEvent(o);
			__instance = o.instance;
			cash = o.cash;
			cashIndex = o.cashIndex;
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;*/

			double cash = c;

			bool allowOriginalMethod = true;
			var p = new SimulationEvents.CashChangedEvent(__instance, cash, CashType.Normal, cashIndex, CashSource.Normal, null);
			EventRegistry.subscriber.dispatchEvent(ref p);
			if (cash > 0)
			{
				var o = new SimulationEvents.CashGainedEvent(__instance, cash, CashType.Normal, cashIndex, CashSource.Normal, null);
				EventRegistry.subscriber.dispatchEvent(ref o);

				cash = o.cash;
				cashIndex = o.cashIndex;
				allowOriginalMethod = !(o.isCancelled() || p.isCancelled());

				return allowOriginalMethod;
			}
			else
			{
				var o = new SimulationEvents.CashLostEvent(__instance, cash, CashType.Normal, cashIndex, CashSource.Normal);
				EventRegistry.subscriber.dispatchEvent(ref o);

				cash = o.cash;
				cashIndex = o.cashIndex;
				allowOriginalMethod = !(o.isCancelled() || p.isCancelled());

				return allowOriginalMethod;
			}
		}
	}
}
