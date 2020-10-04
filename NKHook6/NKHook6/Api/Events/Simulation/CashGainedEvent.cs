using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Towers;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Events._Simulation
{
	public partial class SimulationEvents
	{
		public class CashGainedEvent : EventBaseCancellable
		{
			public Simulation instance;
			public double cash;
			public Simulation.CashType from;
			public int cashIndex;
			public Simulation.CashSource source;
			public Tower tower;

			public CashGainedEvent(ref Simulation __instance, ref double cash, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source, [Optional] ref Tower tower) : base("CashGainedEvent")
			{
				this.instance = __instance;
				this.cash = cash;
				this.from = from;
				this.cashIndex = cashIndex;
				this.source = source;
				this.tower = tower;
			}
		}
	}
}
