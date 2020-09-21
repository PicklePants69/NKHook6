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
        internal static bool Prefix(ref Simulation __instance, int round)
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
        internal static void Postfix(Simulation __instance, int round)
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

        public static event EventHandler<OnRoundEndEventArgs> RoundEnded_Pre;
        public static event EventHandler<OnRoundEndEventArgs> RoundEnded_Post;

        public class OnRoundEndEventArgs : EventArgs
        {
            public Simulation Instance { get; set; }
            public int RoundNumber { get; internal set; }
        }

        public void OnRoundEndPrefix(OnRoundEndEventArgs e)
        {
            EventHandler<OnRoundEndEventArgs> handler = RoundEnded_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnRoundEndPostfix(OnRoundEndEventArgs e)
        {
            EventHandler<OnRoundEndEventArgs> handler = RoundEnded_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
