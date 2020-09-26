using Assets.Scripts.Simulation.Towers.Projectiles;

namespace NKHook6.Api.Events._Projectile
{
	public class OnDestroyEvent
	{
		public class Pre : EventBaseCancellable
		{
			public Projectile instance;

			public Pre(ref Projectile __instance) : base("Projectile.OnDestroyEvent.Pre")
			{
				this.instance = __instance;
			}
		}

		public class Post : EventBase
		{
			public Projectile instance;

			public Post(ref Projectile __instance) : base("Projectile.OnDestroyEvent.Post")
			{
				this.instance = __instance;
			}
		}
	}

}
