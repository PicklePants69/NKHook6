using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class WeaponOnDestroyEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Weapon weapon;

			public Prefix(ref Weapon __instance) : base("WeaponOnDestroyEvent.Pre")
			{
				this.weapon = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public Weapon weapon;

			public Postfix(ref Weapon __instance) : base("WeaponOnDestroyEvent.Post")
			{
				this.weapon = __instance;
			}
		}
	}

}
