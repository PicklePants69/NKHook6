namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    [HarmonyPatch(typeof(Tower), "OnDestroy")]
    class DestroyedHook
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance)
        {
            var o = new TowerEvents.DeletedEvent(ref __instance);
            EventRegistry.subscriber.dispatchEvent(ref o);
            __instance = o.instance;
        }
    }
}
