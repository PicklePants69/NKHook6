using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class WeaponInitialiseEvent
	{
		
		public class Prefix : EventBase
		{
			public bool replaceMethod = false;
			public Weapon weapon;
			public Entity target;
			public Model model;

			public Prefix(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("WeaponInitialiseEvent.Pre")
			{
				this.weapon = __instance;
				this.target = target;
				this.model = modelToUse;
			}
		}

		public class Postfix : EventBase
		{
			public Weapon weapon;
			public Entity target;
			public Model model;

			public Postfix(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("WeaponInitialiseEvent.Post")
			{
				this.weapon = __instance;
				this.target = target;
				this.model = modelToUse;
			}
		}
	}

}
