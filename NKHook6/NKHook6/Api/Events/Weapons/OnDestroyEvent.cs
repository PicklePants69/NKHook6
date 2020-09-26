using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class OnDestroyEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Weapon instance;

			public Pre(ref Weapon __instance) : base("Weapon.OnDestroyEvent.Pre")
			{
				this.instance = __instance;
			}
		}

		public class Post : EventBase
		{
			public Weapon instance;

			public Post(ref Weapon __instance) : base("Weapon.OnDestroyEvent.Post")
			{
				this.instance = __instance;
			}
		}
	}

}
