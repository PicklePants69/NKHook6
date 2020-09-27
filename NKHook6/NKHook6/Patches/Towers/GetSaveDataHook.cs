﻿namespace NKHook6.Patches._Towers
{
    using Assets.Scripts.Models.Profile;
    using Assets.Scripts.Simulation.Towers;
    using Harmony;
    using NKHook6.Api.Events;
    using NKHook6.Api.Events._Towers;

    /// <summary>
    /// Not too sure about this event. I know it fires at the end of each round
    /// </summary>
    [HarmonyPatch(typeof(Tower), "GetSaveData")]
    class GetSaveDataHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new TowerEvents.GetSaveDataEvent.Pre(ref __instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref TowerSaveDataModel __result)
        {
            if (sendPostfixEvent)
            {
                var o = new TowerEvents.GetSaveDataEvent.Post(ref __instance, ref __result);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                __result = o.result;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
