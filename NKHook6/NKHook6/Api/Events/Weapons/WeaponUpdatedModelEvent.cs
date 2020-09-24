using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class WeaponUpdatedModelEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public Weapon weapon;
			public Model model;

			public Prefix(ref Weapon __instance, ref Model modelToUse) : base("WeaponUpdatedModelEvent.Pre")
			{
				this.weapon = __instance;
				this.model = modelToUse;
			}
		}

		public class Postfix : EventBase
		{
			public Weapon weapon;
			public Model model;

			public Postfix(ref Weapon __instance, ref Model modelToUse) : base("WeaponUpdatedModelEvent.Post")
			{
				this.weapon = __instance;
				this.model = modelToUse;
			}
		}
	}

}
