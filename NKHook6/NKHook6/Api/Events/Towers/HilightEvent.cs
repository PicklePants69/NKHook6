using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
	public class HilightEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Tower tower;

			public Pre(ref Tower __instance) : base("Tower.HilightEvent.Pre")
			{
				this.tower = __instance;
			}
		}

		public class Post : EventBase
		{
			public Tower tower;

			public Post(ref Tower __instance) : base("Tower.HilightEvent.Post")
			{
				this.tower = __instance;
			}
		}
	}

}
