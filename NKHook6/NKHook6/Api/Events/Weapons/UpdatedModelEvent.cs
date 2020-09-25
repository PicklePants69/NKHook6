using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.Weapons
{
	public class UpdatedModelEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Weapon weapon;
			public Model model;

			public Pre(ref Weapon __instance, ref Model modelToUse) : base("Weapon.UpdatedModelEvent.Pre")
			{
				this.weapon = __instance;
				this.model = modelToUse;
			}
		}

		public class Post : EventBase
		{
			public Weapon weapon;
			public Model model;

			public Post(ref Weapon __instance, ref Model modelToUse) : base("Weapon.UpdatedModelEvent.Post")
			{
				this.weapon = __instance;
				this.model = modelToUse;
			}
		}
	}

}
