using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Harmony;
using Il2CppSystem.ComponentModel;
using NKHook6.Api.CustomTypes;
using System;
using System.Runtime.InteropServices;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Runtime;

namespace NKHook6.Api.Events.Bloons
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
        internal static bool Prefix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
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
                distributeToChildren = changeDamageInfoTo.DistributeToChildren;
                overrideDistributeBlocker = changeDamageInfoTo.OverrideDistributeBlocker;
                createEffect = changeDamageInfoTo.CreateEffect;
                canDestroyProjectile = changeDamageInfoTo.CanDestroyProjectile;
                ignoreImmunityForBloonTypes = changeDamageInfoTo.IgnoreImmunityForBloonTypes;
                blockSpawnChildren = changeDamageInfoTo.BlockSpawnChildren;

                if (changeDamageInfoTo.BloonInstance != null)
                    __instance = changeDamageInfoTo.BloonInstance;
                if (changeDamageInfoTo.TowerInstance != null)
                    tower = changeDamageInfoTo.TowerInstance;
                if (changeDamageInfoTo.Projectile != null)
                    projectile = changeDamageInfoTo.Projectile;
                if (changeDamageInfoTo.DamageTypes != null && changeDamageInfoTo.DamageTypes.Count > 0)
                    types = changeDamageInfoTo.DamageTypes;
                if (changeDamageInfoTo.IgnoreImmunityForBloonTypes != null && changeDamageInfoTo.IgnoreImmunityForBloonTypes.Count > 0)
                    ignoreImmunityForBloonTypes = changeDamageInfoTo.IgnoreImmunityForBloonTypes;

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
        internal static void Postfix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
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

        public static void ChangeDamageTo(DamageInfo damageInfo) => OnBloonDamaged.changeDamageInfoTo = damageInfo;
        public static void ChangeDamageTo(float damage) => OnBloonDamaged.changeDamageTo = damage;

        public static void ChangeDamageTo(Bloon __instance, float damageAmount, Il2CppStringArray damageTypes,
            Projectile projectile, bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, [Optional]Tower tower,
            [Optional]Il2CppStringArray ignoreImmunityForBloonTypes, bool canDestroyProjectile = true, bool ignoreNonTargetable = false,
            bool blockSpawnChildren = false)
        {
            ChangeDamageTo(new DamageInfo()
            {
                BloonInstance = __instance,
                DamageTaken = damageAmount,
                DamageTypes = damageTypes,
                Projectile = projectile,
                DistributeToChildren = distributeToChildren,
                OverrideDistributeBlocker = overrideDistributeBlocker,
                CreateEffect = createEffect,
                TowerInstance = tower,
                IgnoreImmunityForBloonTypes = ignoreImmunityForBloonTypes,
                CanDestroyProjectile = canDestroyProjectile,
                IgnoreNonTargetables = ignoreNonTargetable,
                BlockSpawnChildren = blockSpawnChildren
            });
        }



        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Pre;
        public static event EventHandler<OnBloonDamagedEventArgs> OnBloonDamaged_Post;

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
            EventHandler<OnBloonDamagedEventArgs> handler = OnBloonDamaged_Pre;
            if (handler != null)
                handler(this, e);
        }

        public void OnBloonDamagedPostfix(OnBloonDamagedEventArgs e)
        {
            EventHandler<OnBloonDamagedEventArgs> handler = OnBloonDamaged_Post;
            if (handler != null)
                handler(this, e);
        }
    }
}
