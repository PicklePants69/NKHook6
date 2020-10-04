namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    [HarmonyPatch(typeof(Tower), "OnSold")]
    class SoldHook
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref float amount)
        {
            bool allowOriginalMethod = true;
            var o = new TowerEvents.SoldEvent(ref __instance, ref amount);
            EventRegistry.subscriber.dispatchEvent(ref o);
            amount = o.sellAmount;
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }
}
