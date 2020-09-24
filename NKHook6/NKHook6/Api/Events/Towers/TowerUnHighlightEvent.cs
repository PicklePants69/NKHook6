using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
	public class TowerUnHighlightEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Tower tower;

			public Prefix(ref Tower __instance) : base("TowerUnHighlightEvent.Pre")
			{
				this.tower = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public Tower tower;

			public Postfix(ref Tower __instance) : base("TowerUnHighlightEvent.Post")
			{
				this.tower = __instance;
			}
		}
	}

}
