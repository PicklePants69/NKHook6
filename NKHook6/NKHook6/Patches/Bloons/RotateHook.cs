using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;

namespace NKHook6.Patches._Bloons
{
    [HarmonyPatch(typeof(Bloon), "SetRotation")]
    class RotateHook
    {
        private static bool sendPrefixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float rotation)
        {
            bool allowOriginalMethod = true;

            var o = new BloonEvents.RotateEvent(ref __instance, ref rotation);
            EventRegistry.subscriber.dispatchEvent(ref o);
            rotation = o.rotation;
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }
}
