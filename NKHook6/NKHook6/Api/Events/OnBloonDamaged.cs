using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Harmony;
using System;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Runtime;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "Damage")]
    public class OnBloonDamaged
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        public static bool Prefix(Bloon __instance, float totalAmount, Il2CppStringArray types, Projectile projectile,
            bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, Tower tower, bool canDestroyProjectile,
            Il2CppStringArray ignoreImmunityForBloonTypes, bool ignoreNonTargetable, bool blockSpawnChildren)
        {
            if (sendPrefixEvent)
            {
                var o = new OnBloonDamaged();
                o.OnBloonDamagedPrefix(Prep(__instance));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(Bloon __instance, float totalAmount, Il2CppStringArray types, Projectile projectile,
            bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, Tower tower, bool canDestroyProjectile,
            Il2CppStringArray ignoreImmunityForBloonTypes, bool ignoreNonTargetable, bool blockSpawnChildren)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonDamaged();
                o.OnBloonDamagedPostfix(Prep(__instance));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        private static OnBloonDamagedEventArgs Prep(Bloon __instance)
        {
            var args = new OnBloonDamagedEventArgs();
            args.Instance = __instance;
            return args;
        }

        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Prefix;
        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Postfix;

        public class OnBloonDamagedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public Entity Target { get; set; }
            public Model Model { get; set; }
        }

        public void OnBloonDamagedPrefix(OnBloonDamagedEventArgs e)
        {
            EventHandler<OnBloonDamagedEventArgs> handler = OnBloonDamaged_Prefix;
            if (handler != null)
                handler(this, e);
        }

        public void OnBloonDamagedPostfix(OnBloonDamagedEventArgs e)
        {
            EventHandler<OnBloonDamagedEventArgs> handler = OnBloonDamaged_Postfix;
            if (handler != null)
                handler(this, e);
        }
    }
}
