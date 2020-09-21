using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using NKHook6.Api.Events;
using NKHook6.Api.CustomTypes;
using System;
using Assets.Scripts.Simulation.Bloons;
using UnhollowerBaseLib;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Simulation.Towers;
using System.Runtime.InteropServices;
using Assets.Scripts.Simulation;

namespace NKHook6.Api.Utilities
{
    public class BloonUtils
    {
        public static void ChangeNextBloonTo(DefaultBloonIds bloonId) => ChangeNextBloonTo(bloonId.ToString());
        public static void ChangeNextBloonTo(string bloonId) => ChangeNextBloonTo(BloonUtils.GetBloon(bloonId));
        public static void ChangeNextBloonTo(BloonModel bloonModel) => OnBloonSpawned.changeBloonToThisModel = bloonModel;


        public static BloonModel GetBloon(DefaultBloonIds bloonId) => GetBloon(bloonId.ToString());

        public static BloonModel GetBloon(string bloonId)
        {
            BloonModel result = null;

            if (Game.instance == null)
            {
                Logger.Log("Can't get BloonModel for BloonId: \"" + bloonId + "\". The Game instance is null");
                return result;
            }
            else if (Game.instance.model == null)
            {
                Logger.Log("Can't get BloonModel for BloonId: \"" + bloonId + "\". The Game instance model is null");
                return result;
            }

            try
            {
                result = Game.instance.model.GetBloon(bloonId);
            }
            catch (Exception e)
            {
                Logger.Log("Exception occured when trying to use GetBloon from NKHook6." +
                    " Tried Getting a bloon with this non-existant BloonId: \"" + bloonId + "\"." +
                    "\nMore exception details: " + e.Message);
            }

            return result;
        }

        public static void ChangeBloonDamageTo(Bloon __instance, float damageAmount)
        {
            
        }

        public static void ChangeBloonDamageTo(Bloon __instance, float damageAmount, Il2CppStringArray damageTypes,
            Projectile projectile, bool distributeToChildren, bool overrideDistributeBlocker, bool createEffect, [Optional]Tower tower,
            [Optional]Il2CppStringArray ignoreImmunityForBloonTypes, bool canDestroyProjectile = true, bool ignoreNonTargetable = false, 
            bool blockSpawnChildren = false)
        {
            ChangeNextBloonDamageTo(new DamageInfo()
            {
                BloonInstance = __instance,
                DamageTaken = damageAmount,
                DamageTypes = damageTypes,
                Projectile = projectile,
                DistributeToChildren = distributeToChildren,
                OverrideDistributeBlocker = overrideDistributeBlocker,
                CreateEffect = createEffect,
                Tower = tower,
                IgnoreImmunityForBloonTypes = ignoreImmunityForBloonTypes,
                CanDestroyProjectile = canDestroyProjectile,
                IgnoreNonTargetables = ignoreNonTargetable,
                BlockSpawnChildren = blockSpawnChildren
            });
        }

        public static void ChangeNextBloonDamageTo(DamageInfo damageInfo) => OnBloonDamaged.changeDamageInfoTo = damageInfo;

        public static void ChangeNextBloonDamageTo(float damage) => OnBloonDamaged.changeDamageTo = damage;
    }
}
