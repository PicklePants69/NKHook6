namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Objects;
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    [HarmonyPatch(typeof(Tower), "Initialise")]
    class CreatedHook
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            var o = new TowerEvents.CreatedEvent(ref __instance, ref target, ref modelToUse);
            EventRegistry.instance.dispatchEvent(ref o);
            target = o.entity;
            modelToUse = o.model;
        }
    }
}
