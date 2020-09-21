using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Factory;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Track;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Utils;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Simulation), "OnRoundStart")]
    public class OnRoundStart
    {
        [HarmonyPrefix]
        public static bool Prefix(Simulation __instance)
        {
            var o = new OnRoundStart();
            o.OnRoundStartPrefix(Prep(__instance));
            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Simulation __instance)
        {
            var o = new OnRoundStart();
            o.OnRoundStartPostfix(Prep(__instance));
        }


        private static OnRoundStartEventArgs Prep(Simulation __instance)
        {
            var args = new OnRoundStartEventArgs();
            args.Instance = __instance;
            return args;
        }

        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Prefix;
        public static event EventHandler<OnRoundStartEventArgs> RoundStarted_Postfix;

        public class OnRoundStartEventArgs : EventArgs
        {
            public Simulation Instance { get; set; }
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
