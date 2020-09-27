using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Towers;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Events._Simulation
{
	public partial class SimulationEvents
	{
		public class AddCashEvent
		{
			public class Pre : EventBaseCancellable
			{
				public Simulation instance;
				public double cash;
				public Simulation.CashType from;
				public int cashIndex;
				public Simulation.CashSource source;
				public Tower tower;

				public Pre(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source, [Optional] ref Tower tower) : base("Simulation.AddCashEvent.Pre")
				{
					this.instance = __instance;
					this.cash = c;
					this.from = from;
					this.cashIndex = cashIndex;
					this.source = source;
					this.tower = tower;
				}
			}

			public class Post : EventBase
			{
				public Simulation instance;
				public double cash;
				public Simulation.CashType from;
				public int cashIndex;
				public Simulation.CashSource source;
				public Tower tower;

				public Post(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source, [Optional] ref Tower tower) : base("Simulation.AddCashEvent.Post")
				{
					this.instance = __instance;
					this.cash = c;
					this.from = from;
					this.cashIndex = cashIndex;
					this.source = source;
					this.tower = tower;
				}
			}
		}
	}
}
