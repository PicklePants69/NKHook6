using Assets.Scripts.Simulation;
using Harmony;
using System;


namespace NKHook6.Api.Events.InGame
{
    [HarmonyPatch(typeof(Simulation), "OnRoundStart")]
    public class OnRoundStart
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Simulation __instance, int roundArrayIndex)
        {
            if (sendPrefixEvent)
            {
                var o = new OnRoundStart();
                o.OnRoundStartPrefix(Prep(__instance, roundArrayIndex));
            }
            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(Simulation __instance, int roundArrayIndex)
        {
            if (sendPostfixEvent)
            {
                var o = new OnRoundStart();
                o.OnRoundStartPostfix(Prep(__instance, roundArrayIndex));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        private static OnRoundStartEventArgs Prep(Simulation __instance, int roundArrayIndex)
        {
            var args = new OnRoundStartEventArgs();
            args.Instance = __instance;
            args.RoundNumber = roundArrayIndex;
            return args;
        }

        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Pre;
        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Post;

        public class OnRoundStartEventArgs : EventArgs
        {
            public Simulation Instance { get; set; }
            public int RoundNumber { get; internal set; }
        }

        public void OnRoundStartPrefix(OnRoundStartEventArgs e)
        {
            EventHandler<OnRoundStartEventArgs> handler = RoundStarted_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnRoundStartPostfix(OnRoundStartEventArgs e)
        {
            EventHandler<OnRoundStartEventArgs> handler = RoundStarted_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
