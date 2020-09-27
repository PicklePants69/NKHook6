using Assets.Scripts.Simulation;
namespace NKHook6.Api.Events._Simulation
{
	public partial class SimulationEvents
	{
		public class TakeDamageEvent
		{
			public class Pre : EventBaseCancellable
			{
				public Simulation instance;
				public float damage;

				public Pre(ref Simulation __instance, ref float damage) : base("Simulation.TakeDamageEvent.Pre")
				{
					this.instance = __instance;
					this.damage = damage;
				}
			}

			public class Post : EventBase
			{
				public Simulation instance;
				public float damage;

				public Post(ref Simulation __instance, ref float damage) : base("Simulation.TakeDamageEvent.Post")
				{
					this.instance = __instance;
					this.damage = damage;
				}
			}
		}
	}
}
