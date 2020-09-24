/*using Assets.Scripts.Simulation.Towers.Weapons;

namespace NKHook6.Api.Events.WeaponBehaviors
{
	public class WeaponBehaviorCanFireEvent
	{
		public class Prefix : EventBaseCancellable
		{
			public WeaponBehavior weaponBehavior;

			public Prefix(ref WeaponBehavior __instance) : base("WeaponBehaviorCanFireEvent.Pre")
			{
				this.weaponBehavior = __instance;
			}
		}

		public class Postfix : EventBase
		{
			public WeaponBehavior weaponBehavior;
			public bool result;

			public Postfix(ref WeaponBehavior __instance, ref bool __result) : base("WeaponBehaviorCanFireEvent.Post")
			{
				this.weaponBehavior = __instance;
				this.result = __result;
			}
		}
	}
}
*/