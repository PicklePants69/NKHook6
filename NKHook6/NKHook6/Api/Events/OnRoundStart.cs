using Assets.Scripts.Simulation;
using Harmony;
using System;


namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Simulation), "OnRoundStart")]
    public class OnRoundStart
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        public static bool Prefix(Simulation __instance, int roundArrayIndex)
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
        public static void Postfix(Simulation __instance, int roundArrayIndex)
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

        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Prefix;
        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Postfix;

        public class OnRoundStartEventArgs : EventArgs
        {
            public Simulation Instance { get; set; }
            public int RoundNumber { get; internal set; }
        }

        public void OnRoundStartPrefix(OnRoundStartEventArgs e)
        {
            EventHandler<OnRoundStartEventArgs> handler = RoundStarted_Prefix;
            if (handler != null)
                handler(this, e);
        }

        public void OnRoundStartPostfix(OnRoundStartEventArgs e)
        {
            EventHandler<OnRoundStartEventArgs> handler = RoundStarted_Postfix;
            if (handler != null)
                handler(this, e);
        }
    }
}
