using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Harmony;
using NKHook6.Api.CustomTypes;
using System;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "OnDestroy")]
    public class OnBloonPopped
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(Bloon __instance)
        {
            if (sendPrefixEvent)
            {
                var o = new OnBloonPopped();
                o.BloonPoppedPrefix(Prep(__instance));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(Bloon __instance)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonPopped();
                o.BloonPoppedPostfix(Prep(__instance));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        private static BloonPoppedEventArgs Prep(Bloon __instance)
        {
            var args = new BloonPoppedEventArgs();
            args.Instance = __instance;
            return args;
        }

        public static event EventHandler<BloonPoppedEventArgs> BloonPopped_Pre;
        public static event EventHandler<BloonPoppedEventArgs> BloonPopped_Post;

        public class BloonPoppedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public Entity Target { get; set; }
            public Model Model { get; set; }
        }

        public void BloonPoppedPrefix(BloonPoppedEventArgs e)
        {
            EventHandler<BloonPoppedEventArgs> handler = BloonPopped_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void BloonPoppedPostfix(BloonPoppedEventArgs e)
        {
            EventHandler<BloonPoppedEventArgs> handler = BloonPopped_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
