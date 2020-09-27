using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;

namespace NKHook6.Patches._Bloons
{
    [HarmonyPatch(typeof(Bloon), "SetRotation")]
    class SetRotationHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float rotation)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new BloonEvents.SetRotationEvent.Pre(ref __instance, ref rotation);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                rotation = o.rotation;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref float rotation)
        {
            if (sendPostfixEvent)
            {
                var o = new BloonEvents.SetRotationEvent.Post(ref __instance, ref rotation);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                rotation = o.rotation;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
