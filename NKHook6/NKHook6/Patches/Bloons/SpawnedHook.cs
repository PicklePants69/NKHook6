using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Bloons;

namespace NKHook6.Patches.Bloons
{
    [HarmonyPatch(typeof(Bloon), "Initialise")]
    class SpawnedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref Entity target, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new SpawnedEvent.Pre(ref __instance, ref target, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                target = o.target;
                modelToUse = o.model;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref Entity target, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new SpawnedEvent.Post(ref __instance, ref target, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.bloon;
                target = o.target;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
