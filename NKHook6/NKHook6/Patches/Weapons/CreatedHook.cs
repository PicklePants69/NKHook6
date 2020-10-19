namespace NKHook6.Patches._Weapons
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers.Weapons;
    using Harmony;
	using NKHook6.Api.Events;
    using NKHook6.Api.Events._Weapons;

    [HarmonyPatch(typeof(Weapon), "Initialise")]
	class CreatedHook
	{
		[HarmonyPostfix]
		internal static void Postfix(ref Weapon __instance, ref Entity target, ref Model modelToUse)
		{
			var o = new WeaponEvents.CreatedEvent(ref __instance, ref target, ref modelToUse);
			EventRegistry.instance.dispatchEvent(ref o);
			target = o.entity;
			modelToUse = o.model;
		}
	}
}
