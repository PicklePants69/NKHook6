using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Harmony;
using NKHook6.Api.CustomTypes;
using System;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Runtime;

namespace NKHook6.Api.Events
{
    [HarmonyPatch(typeof(Bloon), "Damage", new Type[] { typeof(float), typeof(Il2CppStringArray), typeof(Projectile), typeof(bool), 
        typeof(bool), typeof(bool), typeof(Tower), typeof(bool), typeof(Il2CppStringArray), typeof(bool), typeof(bool) })]
    public class OnBloonDamaged
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        //can use either one of these
        internal static DamageInfo changeDamageInfoTo = null;
        internal static float changeDamageTo = -99999999;

        [HarmonyPrefix]
        public static bool Prefix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
            ref bool distributeToChildren, ref bool overrideDistributeBlocker, ref bool createEffect, ref Tower tower, 
            ref bool canDestroyProjectile, ref Il2CppStringArray ignoreImmunityForBloonTypes, ref bool ignoreNonTargetable,
            ref bool blockSpawnChildren)
        {
            if (changeDamageTo != -99999999) 
            {
                totalAmount = changeDamageTo;
                changeDamageTo = -99999999;
            }
            else if (changeDamageInfoTo != null)
            {
                totalAmount = changeDamageInfoTo.DamageTaken;
                changeDamageInfoTo = null;
            }

            if (sendPrefixEvent)
            {
                var o = new OnBloonDamaged();
                o.OnBloonDamagedPrefix(Prep(__instance, totalAmount, types, projectile, distributeToChildren,
                    overrideDistributeBlocker, createEffect, tower, canDestroyProjectile, ignoreImmunityForBloonTypes,
                    ignoreNonTargetable, blockSpawnChildren));
            }

            sendPrefixEvent = !sendPrefixEvent;

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
            ref bool distributeToChildren, ref bool overrideDistributeBlocker, ref bool createEffect, ref Tower tower,
            ref bool canDestroyProjectile, ref Il2CppStringArray ignoreImmunityForBloonTypes, ref bool ignoreNonTargetable,
            ref bool blockSpawnChildren)
        {
            if (sendPostfixEvent)
            {
                var o = new OnBloonDamaged();
                o.OnBloonDamagedPostfix(Prep(__instance, totalAmount, types, projectile, distributeToChildren, 
                    overrideDistributeBlocker, createEffect, tower, canDestroyProjectile, ignoreImmunityForBloonTypes, 
                    ignoreNonTargetable, blockSpawnChildren));
            }

            sendPostfixEvent = !sendPostfixEvent;
        }


        private static OnBloonDamagedEventArgs Prep(Bloon __instance, float totalAmount, Il2CppStringArray types, Projectile projectile,
            bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, Tower tower, bool canDestroyProjectile,
            Il2CppStringArray ignoreImmunityForBloonTypes, bool ignoreNonTargetable, bool blockSpawnChildren)
        {
            var args = new OnBloonDamagedEventArgs();
            args.Instance = __instance;
            args.DamageTaken = totalAmount;
            args.DamageTypes = types;
            args.Projectile = projectile;
            args.DistributeToChildren = distributeToChildren;
            args.OverrideDistributeBlocker = overrideDistributeBlocker;
            args.CreateEffect = createEffect;
            args.Tower = tower;
            args.CanDestroyProjectile = canDestroyProjectile;
            args.IgnoreImmunityForBloonTypes = ignoreImmunityForBloonTypes;
            args.IgnoreNonTargetables = ignoreNonTargetable;
            args.BlockSpawnChildren = blockSpawnChildren;
            return args;
        }

        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Prefix;
        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Postfix;

        public class OnBloonDamagedEventArgs : EventArgs
        {
            public Bloon Instance { get; set; }
            public float DamageTaken { get; set; }
            public Il2CppStringArray DamageTypes { get; set; }
            public Projectile Projectile { get; set; }
            public bool DistributeToChildren { get; set; }
            public bool OverrideDistributeBlocker { get; set; }
            public bool CreateEffect { get; set; }
            public Tower Tower { get; set; }
            public bool CanDestroyProjectile { get; set; }
            public Il2CppStringArray IgnoreImmunityForBloonTypes { get; set; }
            public bool IgnoreNonTargetables { get; set; }
            public bool BlockSpawnChildren { get; set; }
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
