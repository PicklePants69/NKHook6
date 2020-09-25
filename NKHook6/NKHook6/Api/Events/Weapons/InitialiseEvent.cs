using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class InitialiseEvent
	{
		
		public class Pre : EventBaseCancellable
		{
			public Weapon weapon;
			public Entity target;
			public Model model;

			public Pre(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("Weapon.InitialiseEvent.Pre")
			{
				this.weapon = __instance;
				this.target = target;
				this.model = modelToUse;
			}
		}

		public class Post : EventBase
		{
			public Weapon weapon;
			public Entity target;
			public Model model;

			public Post(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("Weapon.InitialiseEvent.Post")
			{
				this.weapon = __instance;
				this.target = target;
				this.model = modelToUse;
			}
		}
	}

}
