namespace NKHook6.Patches._Projectile
{
    using Assets.Scripts.Simulation.Towers.Projectiles;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Projectile;

    [HarmonyPatch(typeof(Projectile), "OnDestroy")]
	class OnDestroyHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Projectile __instance)
		{
			bool allowOriginalMethod = true;
			var o = new ProjectileEvents.DeletedEvent(__instance);
			EventRegistry.instance.dispatchEvent(ref o);
			__instance = o.instance;
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;
		}
	}
}
