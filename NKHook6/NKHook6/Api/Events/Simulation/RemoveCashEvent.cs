using Assets.Scripts.Simulation;

namespace NKHook6.Api.Events._Simulation
{
	public partial class SimulationEvents
	{
		public class RemoveCashEvent
		{
			public class Pre : EventBaseCancellable
			{
				public Simulation instance;
				public double cash;
				public Simulation.CashType from;
				public int cashIndex;
				public Simulation.CashSource source;

				public Pre(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source) : base("Simulation.RemoveCashEvent.Pre")
				{
					this.instance = __instance;
					this.cash = c;
					this.from = from;
					this.cashIndex = cashIndex;
					this.source = source;
				}
			}

			public class Post : EventBase
			{
				public Simulation instance;
				public double cash;
				public Simulation.CashType from;
				public int cashIndex;
				public Simulation.CashSource source;

				public Post(ref Simulation __instance, ref double c, ref Simulation.CashType from, ref int cashIndex, ref Simulation.CashSource source) : base("Simulation.RemoveCashEvent.Post")
				{
					this.instance = __instance;
					this.cash = c;
					this.from = from;
					this.cashIndex = cashIndex;
					this.source = source;
				}
			}
		}
	}
}
