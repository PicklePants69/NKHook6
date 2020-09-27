using Assets.Scripts.Simulation;
namespace NKHook6.Api.Events._Simulation
{
	public class OnDefeatEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Simulation instance;

			public Pre(ref Simulation __instance) : base("Simulation.OnDefeatEvent.Pre")
			{
				this.instance = __instance;
			}
		}

		public class Post : EventBase
		{
			public Simulation instance;

			public Post(ref Simulation __instance) : base("Simulation.OnDefeatEvent.Post")
			{
				this.instance = __instance;
			}
		}
	}

}
