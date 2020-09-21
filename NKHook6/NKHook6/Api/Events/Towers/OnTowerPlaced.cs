using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Harmony;
using System;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "Initialise")]
    public class OnOnTowerPlaced
    {
        internal static BloonModel changeBloonToThisModel;
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(Bloon __instance, ref Entity target, ref Model modelToUse)
        {
            if (changeBloonToThisModel != null)
            {
                modelToUse = changeBloonToThisModel;
                changeBloonToThisModel = null;
            }

            if (sendPrefixEvent)
            {
                var o = new OnOnTowerPlaced();
                o.OnTowerPlacedPrefix(Prep(__instance, target, modelToUse));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(Bloon __instance, Entity target, Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new OnOnTowerPlaced();
                o.OnTowerPlacedPostfix(Prep(__instance, target, modelToUse));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }





        private static OnTowerPlacedEventArgs Prep(Bloon __instance, Entity target, Model modelToUse)
        {
            var args = new OnTowerPlacedEventArgs();
            args.Instance = __instance;
            args.Target = target;
            args.Model = modelToUse;
            return args;
        }

        public static event EventHandler<OnTowerPlacedEventArgs> OnTowerPlaced_Pre;
        public static event EventHandler<OnTowerPlacedEventArgs> OnTowerPlaced_Post;

        public class OnTowerPlacedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public Entity Target { get; set; }
            public Model Model { get; set; }
        }

        public void OnTowerPlacedPrefix(OnTowerPlacedEventArgs e)
        {
            EventHandler<OnTowerPlacedEventArgs> handler = OnTowerPlaced_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnTowerPlacedPostfix(OnTowerPlacedEventArgs e)
        {
            EventHandler<OnTowerPlacedEventArgs> handler = OnTowerPlaced_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
