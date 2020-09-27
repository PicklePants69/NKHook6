using Assets.Scripts.Simulation;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Events._Simulation
{
	public class SetCashEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Simulation instance;
			public double cash;
			public int cashIndex;

			public Pre(ref Simulation __instance, ref double c, [Optional]ref int cashIndex) : base("Simulation.SetCashEvent.Pre")
			{
				this.instance = __instance;
				this.cash = c;
				this.cashIndex = cashIndex;
			}
		}

		public class Post : EventBase
		{
			public Simulation instance;
			public double cash;
			public int cashIndex;

			public Post(ref Simulation __instance, ref double c, [Optional]ref int cashIndex) : base("Simulation.SetCashEvent.Post")
			{
				this.instance = __instance;
				this.cash = c;
				this.cashIndex = cashIndex;
			}
		}
	}

}
