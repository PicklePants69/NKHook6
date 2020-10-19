namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Models;
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    [HarmonyPatch(typeof(Tower), "UpdatedModel")]
    class UpdatedModelHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerEvents.UpdatedModelEvent.Pre(ref __instance, ref modelToUse);
                EventRegistry.instance.dispatchEvent(ref o);
                __instance = o.instance;
                modelToUse = o.model;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerEvents.UpdatedModelEvent.Post(ref __instance, ref modelToUse);
                EventRegistry.instance.dispatchEvent(ref o);
                __instance = o.instance;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
