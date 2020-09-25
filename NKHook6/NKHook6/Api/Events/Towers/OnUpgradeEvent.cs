using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
	public class OnUpgradeEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Tower tower;

			public Pre(ref Tower __instance) : base("Tower.OnUpgradeEvent.Pre")
			{
				this.tower = __instance;
			}
		}

		public class Post : EventBase
		{
			public Tower tower;

			public Post(ref Tower __instance) : base("Tower.OnUpgradeEvent.Post")
			{
				this.tower = __instance;
			}
		}
	}

}
