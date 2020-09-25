using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class OnDestroyEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Weapon weapon;

			public Pre(ref Weapon __instance) : base("Weapon.OnDestroyEvent.Pre")
			{
				this.weapon = __instance;
			}
		}

		public class Post : EventBase
		{
			public Weapon weapon;

			public Post(ref Weapon __instance) : base("Weapon.OnDestroyEvent.Post")
			{
				this.weapon = __instance;
			}
		}
	}

}
