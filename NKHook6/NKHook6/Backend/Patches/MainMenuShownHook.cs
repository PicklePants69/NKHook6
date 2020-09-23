using Assets.Scripts.Unity.UI_New.Main;
using Harmony;
using NKHook6.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Backend.Patches
{
    [HarmonyPatch(typeof(MainMenu), "OnEnable")]
    class MainMenuShownHook
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

            if (sendPrefixEvent)
            {
                var o = new MainMenuShownEvent.Prefix(__instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(MainMenu __instance)
        {
            postfixFireCount++;
            if (postfixFireCount <= skipPostfixCount)
                return;

            if (sendPostfixEvent)
            {
                var o = new MainMenuShownEvent.Postfix(__instance);
                EventRegistry.subscriber.dispatchEvent(ref o);
            }

            sendPostfixEvent = !sendPostfixEvent;
        }

        private static OnMainMenuShownEventArgs Prep(MainMenu __instance)
        {
            var args = new OnMainMenuShownEventArgs();
            args.Instance = __instance;
            return args;
        }

        public class OnMainMenuShownEventArgs : EventArgs
        {
            public MainMenu Instance { get; set; }
        }
    }
}
