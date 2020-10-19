using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;

namespace NKHook6.Patches._Bloons
{
    /*[HarmonyPatch(typeof(Bloon), "set_distanceTraveled")]
    class MoveHook
    {
        private static bool sendPrefixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float newPosition)
        {
            bool allowOriginalMethod = true;

            float oldPosition = __instance.distanceTraveled;
            var o = new BloonEvents.MoveEvent(ref __instance, ref newPosition, ref oldPosition);
            EventRegistry.instance.dispatchEvent(ref o);
            newPosition = o.newPosition;
            allowOriginalMethod = !o.isCancelled();

            return allowOriginalMethod;
        }
    }*/
}
