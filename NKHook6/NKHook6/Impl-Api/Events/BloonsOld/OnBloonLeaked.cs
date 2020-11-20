using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Bloons;
using Harmony;
using System;

namespace NKHook6.Api.Events.Bloons
{
    [HarmonyPatch(typeof(Bloon), "Leaked")]
    public class OnBloonLeaked
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance)
        {
            if (sendPrefixEvent)
            {
                var o = new OnBloonLeaked();
                o.OnBloonLeakedPrefix(Prep(ref __instance));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonLeaked();
                o.OnBloonLeakedPostfix(Prep(ref __instance));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }

        private static OnBloonLeakedEventArgs Prep(ref Bloon __instance)
        {
            var args = new OnBloonLeakedEventArgs();
            args.Instance = __instance;
            args.LeakDamage = __instance.GetModifiedTotalLeakDamage();
            return args;
        }


        public static event EventHandler<OnBloonLeakedEventArgs> BloonLeaked_Pre;
        public static event EventHandler<OnBloonLeakedEventArgs> BloonLeaked_Post;

        public class OnBloonLeakedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public float LeakDamage { get; set; }
        }

        public void OnBloonLeakedPrefix(OnBloonLeakedEventArgs e)
        {
            EventHandler<OnBloonLeakedEventArgs> handler = BloonLeaked_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnBloonLeakedPostfix(OnBloonLeakedEventArgs e)
        {
            EventHandler<OnBloonLeakedEventArgs> handler = BloonLeaked_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
