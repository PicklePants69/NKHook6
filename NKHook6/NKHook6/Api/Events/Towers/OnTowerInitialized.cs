using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using NKHook6.Api.Enums;
using NKHook6.Api.Utilities;
using System;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Events.Towers
{
    [HarmonyPatch(typeof(Tower), "Initialise")]
    public class OnTowerInitialized
    {
        internal static Model changeTowerToThisModel;
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            if (changeTowerToThisModel != null)
            {
                modelToUse = changeTowerToThisModel;
                changeTowerToThisModel = null;
            }

            if (sendPrefixEvent)
            {
                var o = new OnTowerInitialized();
                o.OnTowerInitializedPrefix(Prep(ref __instance, ref target, ref modelToUse));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            if (sendPostfixEvent)
            {
                var o = new OnTowerInitialized();
                o.OnTowerInitializedPostfix(Prep(ref __instance, ref target, ref modelToUse));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        public static void SetTower(DefaultTowerIds baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => SetTower(TowerUtils.GetTower(baseId.ToString(), tier1, tier2, tier3));

        public static void SetTower(string baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => SetTower(TowerUtils.GetTower(baseId, tier1, tier2, tier3));

        public static void SetTower(TowerModel towerToUse)
        {
            if (towerToUse == null)
            {
                Logger.Log("Error! Failed to set tower because the new TowerModel was null!");
                return;
            }

            changeTowerToThisModel = towerToUse;
        }



        private static OnTowerInitializedEventArgs Prep(ref Tower __instance, ref Entity target, ref Model modelToUse)
        {
            var args = new OnTowerInitializedEventArgs();
            args.Instance = __instance;
            args.Target = target;
            args.ModelToUse = modelToUse;
            return args;
        }

        public static event EventHandler<OnTowerInitializedEventArgs> OnTowerInitialized_Pre;
        public static event EventHandler<OnTowerInitializedEventArgs> OnTowerInitialized_Post;

        public class OnTowerInitializedEventArgs : EventArgs
        {
            public Tower Instance { get; set; }
            public Entity Target { get; set; }
            public Model ModelToUse { get; set; }
        }

        public void OnTowerInitializedPrefix(OnTowerInitializedEventArgs e)
        {
            EventHandler<OnTowerInitializedEventArgs> handler = OnTowerInitialized_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnTowerInitializedPostfix(OnTowerInitializedEventArgs e)
        {
            EventHandler<OnTowerInitializedEventArgs> handler = OnTowerInitialized_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
