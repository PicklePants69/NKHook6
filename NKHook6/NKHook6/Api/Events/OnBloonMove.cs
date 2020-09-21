using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Bloons;
using Harmony;
using System;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "Move")]
    public class OnBloonMove
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;
        private static float changeAmountToMove = -99999999;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float byAmount)
        {
            byAmount += 20;
            if (changeAmountToMove != -99999999)
            {
                byAmount = changeAmountToMove;
                changeAmountToMove = -99999999;
            }

            if (sendPrefixEvent)
            {
                var o = new OnBloonMove();
                o.OnBloonMovePrefix(Prep(ref __instance, ref byAmount));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Bloon __instance, ref float byAmount)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonMove();
                o.OnBloonMovePostfix(Prep(ref __instance, ref byAmount));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }

        private static OnBloonMoveEventArgs Prep(ref Bloon __instance, ref float byAmount)
        {
            var args = new OnBloonMoveEventArgs();
            args.Instance = __instance;
            args.AmountToMove = byAmount;
            return args;
        }
        
        public static void ChangeAmountToMove(float amount) => changeAmountToMove = amount;


        public static event EventHandler<OnBloonMoveEventArgs> BloonMove_Pre;
        public static event EventHandler<OnBloonMoveEventArgs> BloonMove_Post;

        public class OnBloonMoveEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public float AmountToMove { get; set; }
        }

        public void OnBloonMovePrefix(OnBloonMoveEventArgs e)
        {
            EventHandler<OnBloonMoveEventArgs> handler = BloonMove_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnBloonMovePostfix(OnBloonMoveEventArgs e)
        {
            EventHandler<OnBloonMoveEventArgs> handler = BloonMove_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
