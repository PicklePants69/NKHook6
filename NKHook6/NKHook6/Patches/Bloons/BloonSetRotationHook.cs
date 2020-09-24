using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Bloons;

namespace NKHook6.Patches.Bloons
{
    [HarmonyPatch(typeof(Bloon), "SetRotation")]
    class BloonSetRotationHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float rotation)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new BloonSetRotationEvent.Prefix(ref __instance, ref rotation);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                rotation = o.rotation;
                allowOriginalMethod = !o.replaceMethod;
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref float rotation)
        {
            if (sendPostfixEvent)
            {
                var o = new BloonSetRotationEvent.Postfix(ref __instance, ref rotation);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                rotation = o.rotation;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
