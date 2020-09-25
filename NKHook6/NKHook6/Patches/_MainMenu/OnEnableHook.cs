using Assets.Scripts.Unity.UI_New.Main;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;

namespace NKHook6.Backend.Patches._MainMenu
{
    [HarmonyPatch(typeof(MainMenu), "OnEnable")]
    class OnEnableHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        //Need this weird skip stuff because MainMenu.OnEnable gets called 4 times when it gets to main menu for first time
        const int skipPrefixCount = 2;
        const int skipPostfixCount = 2;
        private static int prefixFireCount = 0;
        private static int postfixFireCount = 0;

        [HarmonyPrefix]
        internal static bool Prefix(MainMenu __instance)
        {
            prefixFireCount++;
            if (prefixFireCount <= skipPrefixCount)
                return true;

            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new OnEnableEvent.Pre(__instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.mainMenu;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(MainMenu __instance)
        {
            postfixFireCount++;
            if (postfixFireCount <= skipPostfixCount)
                return;

            if (sendPostfixEvent)
            {
                var o = new OnEnableEvent.Post(__instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.mainMenu;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
