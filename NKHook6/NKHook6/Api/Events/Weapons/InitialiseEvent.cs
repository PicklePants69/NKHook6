using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events._Weapons
{
	public partial class Weapon
	{
		public class InitialiseEvent
		{

			public class Pre : EventBaseCancellable
			{
				public Weapon instance;
				public Entity entity;
				public Model model;

				public Pre(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("Weapon.InitialiseEvent.Pre")
				{
					this.instance = __instance;
					this.entity = target;
					this.model = modelToUse;
				}
			}

			public class Post : EventBase
			{
				public Weapon instance;
				public Entity entity;
				public Model model;

				public Post(ref Weapon __instance, ref Entity target, ref Model modelToUse) : base("Weapon.InitialiseEvent.Post")
				{
					this.instance = __instance;
					this.entity = target;
					this.model = modelToUse;
				}
			}
		}
	}
}
