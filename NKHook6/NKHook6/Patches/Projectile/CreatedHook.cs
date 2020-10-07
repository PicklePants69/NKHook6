namespace NKHook6.Patches._Projectile
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers.Projectiles;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Projectile;

    [HarmonyPatch(typeof(Projectile), "Initialise")]
	class CreatedHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Projectile __instance, ref Entity target, ref Model modelToUse)
		{
			bool allowOriginalMethod = true;
			var o = new ProjectileEvents.CreatedEvent(__instance, target, modelToUse);
			EventRegistry.subscriber.dispatchEvent(ref o);
			__instance = o.instance;
			target = o.entity;
			modelToUse = o.model;
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;
		}
	}
}
