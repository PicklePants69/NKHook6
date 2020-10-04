namespace NKHook6.Patches._Weapons
{
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Weapons;

    [HarmonyPatch(typeof(Weapon), "OnDestroy")]
	class DeletedHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Weapon __instance)
		{
			bool allowOriginalMethod = true;
			var o = new WeaponEvents.DeletedEvent(ref __instance);
			EventRegistry.subscriber.dispatchEvent(ref o);
			allowOriginalMethod = !o.isCancelled();

			return allowOriginalMethod;
		}
	}
}
