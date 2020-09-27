using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events.Towers;

namespace NKHook6.Patches.Towers
{
    [HarmonyPatch(typeof(Tower), "Initialise")]
    class InitializedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new InitialisedEvent.Pre(ref __instance, ref target, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                target = o.entity;
                modelToUse = o.model;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new InitialisedEvent.Post(ref __instance, ref target, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                target = o.entity;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
