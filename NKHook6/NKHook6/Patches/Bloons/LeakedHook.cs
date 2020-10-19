using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;

namespace NKHook6.Patches._Bloons
{
    [HarmonyPatch(typeof(Bloon), "Leaked")]
    class LeakedHook
    {
        private static bool sendPrefixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance)
        {
            bool allowOriginalMethod = true;

            var o = new BloonEvents.LeakedEvent(ref __instance);
            EventRegistry.instance.dispatchEvent(ref o);
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }
}
