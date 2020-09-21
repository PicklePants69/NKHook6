using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Harmony;
using NKHook6.Api.Utilities;
using System;
using System.Security.Policy;
using UnityEngine;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "Initialise")]
    public class OnBloonSpawned
    {
        internal static BloonModel changeBloonToThisModel;
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        public static bool Prefix(Bloon __instance, ref Entity target, ref Model modelToUse)
        {
            if (changeBloonToThisModel != null)
            {
                modelToUse = changeBloonToThisModel;
                changeBloonToThisModel = null;
            }

            if (sendPrefixEvent)
            {
                var o = new OnBloonSpawned();
                o.BloonSpawnedPrefix(Prep(__instance, target, modelToUse));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Bloon __instance, Entity target, Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonSpawned();
                o.BloonSpawnedPostfix(Prep(__instance, target, modelToUse));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }

        
        


        private static BloonSpawnedEventArgs Prep(Bloon __instance, Entity target, Model modelToUse)
        {
            var args = new BloonSpawnedEventArgs();
            args.Instance = __instance;
            args.Target = target;
            args.Model = modelToUse;
            return args;
        }

        public static event EventHandler<BloonSpawnedEventArgs> BloonSpawned_Prefix;
        public static event EventHandler<BloonSpawnedEventArgs> BloonSpawned_Postfix;

        public class BloonSpawnedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public Entity Target { get; set; }
            public Model Model { get; set; }
        }

        public void BloonSpawnedPrefix(BloonSpawnedEventArgs e)
        {
            EventHandler<BloonSpawnedEventArgs> handler = BloonSpawned_Prefix;
            if (handler != null)
                handler(this, e);
        }

        public void BloonSpawnedPostfix(BloonSpawnedEventArgs e)
        {
            EventHandler<BloonSpawnedEventArgs> handler = BloonSpawned_Postfix;
            if (handler != null)
                handler(this, e);
        }
    }
}
