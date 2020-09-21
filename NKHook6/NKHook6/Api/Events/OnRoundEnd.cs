using Assets.Scripts.Simulation;
using Harmony;
using System;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Simulation), "OnRoundEnd")]
    public class OnRoundEnd
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        public static bool Prefix(Simulation __instance, int round)
        {
            if (sendPrefixEvent)
            {
                var o = new OnRoundEnd();
                o.OnRoundEndPrefix(Prep(__instance, round));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Simulation __instance, int round)
        {
            if (sendPostfixEvent)
            {
                var o = new OnRoundEnd();
                o.OnRoundEndPostfix(Prep(__instance, round));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        private static OnRoundEndEventArgs Prep(Simulation __instance, int round)
        {
            var args = new OnRoundEndEventArgs();
            args.Instance = __instance;
            args.RoundNumber = round;
            return args;
        }

        public static event EventHandler<OnRoundEndEventArgs> RoundEnded_Prefix;
        public static event EventHandler<OnRoundEndEventArgs> RoundEnded_Postfix;

        public class OnRoundEndEventArgs : EventArgs
        {
            public Simulation Instance { get; set; }
            public int RoundNumber { get; internal set; }
        }

        public void OnRoundEndPrefix(OnRoundEndEventArgs e)
        {
            EventHandler<OnRoundEndEventArgs> handler = RoundEnded_Prefix;
            if (handler != null)
                handler(this, e);
        }

        public void OnRoundEndPostfix(OnRoundEndEventArgs e)
        {
            EventHandler<OnRoundEndEventArgs> handler = RoundEnded_Postfix;
            if (handler != null)
                handler(this, e);
        }
    }
}
