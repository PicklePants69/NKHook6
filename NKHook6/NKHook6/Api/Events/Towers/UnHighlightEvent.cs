using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
	public class UnHighlightEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Tower tower;

			public Pre(ref Tower __instance) : base("Tower.UnHighlightEvent.Pre")
			{
				this.tower = __instance;
			}
		}

		public class Post : EventBase
		{
			public Tower tower;

			public Post(ref Tower __instance) : base("Tower.UnHighlightEvent.Post")
			{
				this.tower = __instance;
			}
		}
	}

}
