using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity.UI_New.Main;
using Harmony;
using System;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(MainMenu), "OnEnable")]
    public class OnMainMenuShown
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
                var o = new OnMainMenuShown();
                o.OnMainMenuShownPrefix(Prep(__instance));
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
                var o = new OnMainMenuShown();
                o.OnMainMenuShownPostfix(Prep(__instance));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }

        private static OnMainMenuShownEventArgs Prep(MainMenu __instance)
        {
            var args = new OnMainMenuShownEventArgs();
            args.Instance = __instance;
            return args;
        }


        public static event EventHandler<OnMainMenuShownEventArgs> MainMenuShown_Pre;
        public static event EventHandler<OnMainMenuShownEventArgs> MainMenuShown_Post;

        public class OnMainMenuShownEventArgs : EventArgs
        {
            public MainMenu Instance { get; set; }
        }

        public void OnMainMenuShownPrefix(OnMainMenuShownEventArgs e)
        {
            EventHandler<OnMainMenuShownEventArgs> handler = MainMenuShown_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnMainMenuShownPostfix(OnMainMenuShownEventArgs e)
        {
            EventHandler<OnMainMenuShownEventArgs> handler = MainMenuShown_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
