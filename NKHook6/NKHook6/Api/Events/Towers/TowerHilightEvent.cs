using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
	public class TowerHilightEvent
	{
		public class Prefix : EventBase
		{
			public bool replaceMethod = false;
			public Tower tower;

			public Prefix(ref Tower __instance) : base("TowerHilightEvent.Pre")
			{
				this.tower = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public Tower tower;

			public Postfix(ref Tower __instance) : base("TowerHilightEvent.Post")
			{
				this.tower = __instance;
			}
		}
	}

}
