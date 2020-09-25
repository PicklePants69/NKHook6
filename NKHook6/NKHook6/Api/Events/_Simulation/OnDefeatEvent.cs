using Assets.Scripts.Simulation;
namespace NKHook6.Api.Events._Simulation
{
	public class OnDefeatEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Simulation simulation;

			public Pre(ref Simulation __instance) : base("Simulation.OnDefeatEvent.Pre")
			{
				this.simulation = __instance;
			}
		}

		public class Post : EventBase
		{
			public Simulation simulation;

			public Post(ref Simulation __instance) : base("Simulation.OnDefeatEvent.Post")
			{
				this.simulation = __instance;
			}
		}
	}

}
