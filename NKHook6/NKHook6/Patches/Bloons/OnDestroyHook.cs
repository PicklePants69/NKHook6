using Assets.Scripts.Simulation.Bloons;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Bloons;

namespace NKHook6.Patches.Bloons
{
    [HarmonyPatch(typeof(Bloon), "OnDestroy")]
    class OnDestroyHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new OnDestroyEvent.Pre(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance)
        {
            if (sendPostfixEvent)
            {
                var o = new OnDestroyEvent.Post(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
