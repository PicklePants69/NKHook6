using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;

namespace NKHook6.Patches._Bloons
{
    [HarmonyPatch(typeof(Bloon), "Initialise")]
    class InitialiseHook
    {
        private static bool sendPostfixEvent = true;

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref Entity target, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new BloonEvents.CreatedEvent(ref __instance, ref target, ref modelToUse);
                EventRegistry.subscriber.dispatchEvent(ref o);
                target = o.entity;
                modelToUse = o.model;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
