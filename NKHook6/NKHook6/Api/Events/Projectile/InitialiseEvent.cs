using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers.Projectiles;

namespace NKHook6.Api.Events._Projectile
{
	public partial class ProjectileEvents
	{
		public class InitialiseEvent
		{
			public class Pre : EventBaseCancellable
			{
				public Projectile instance;
				public Entity entity;
				public Model model;

				public Pre(ref Projectile __instance, ref Entity target, ref Model modelToUse) : base("Projectile.InitialiseEvent.Pre")
				{
					this.instance = __instance;
					this.entity = target;
					this.model = modelToUse;
				}
			}

			public class Post : EventBase
			{
				public Projectile instance;
				public Entity entity;
				public Model model;

				public Post(ref Projectile __instance, ref Entity target, ref Model modelToUse) : base("Projectile.InitialiseEvent.Post")
				{
					this.instance = __instance;
					this.entity = target;
					this.model = modelToUse;
				}
			}
		}
	}
}
