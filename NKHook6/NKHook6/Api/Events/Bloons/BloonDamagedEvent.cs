using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using UnhollowerBaseLib;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonDamagedEvent
    {
        public class Prefix : EventBase
        {
            public Bloon bloon;
            public float damageTaken { get; set; }
            public Il2CppStringArray damageTypes { get; set; }
            public Projectile projectile { get; set; }
            public bool distrubuteToChildren { get; set; }
            public bool overrideDistributeBlocker { get; set; }
            public bool createEffect { get; set; }
            public Tower tower { get; set; }
            public bool canDestroyProjectile { get; set; }
            public Il2CppStringArray ignoreImmunityForBloonTypes { get; set; }
            public bool ignoreNonTargetables { get; set; }
            public bool blockSpawnChildren { get; set; }
            public bool replaceMethod { get; set; }

            public Prefix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
            ref bool distributeToChildren, ref bool overrideDistributeBlocker, ref bool createEffect, ref Tower tower,
            ref bool canDestroyProjectile, ref Il2CppStringArray ignoreImmunityForBloonTypes, ref bool ignoreNonTargetable,
            ref bool blockSpawnChildren) : base("BloonDamagedEvent.Pre")
            {
                this.bloon = __instance;
                this.damageTaken = totalAmount;
                this.damageTypes = types;
                this.projectile = projectile;
                this.distrubuteToChildren = distributeToChildren;
                this.overrideDistributeBlocker = overrideDistributeBlocker;
                this.createEffect = createEffect;
                this.tower = tower;
                this.canDestroyProjectile = canDestroyProjectile;
                this.ignoreImmunityForBloonTypes = ignoreImmunityForBloonTypes;
                this.ignoreNonTargetables = ignoreNonTargetable;
                this.blockSpawnChildren = blockSpawnChildren;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;
            public float damageTaken { get; set; }
            public Il2CppStringArray damageTypes { get; set; }
            public Projectile projectile { get; set; }
            public bool distrubuteToChildren { get; set; }
            public bool overrideDistributeBlocker { get; set; }
            public bool createEffect { get; set; }
            public Tower tower { get; set; }
            public bool canDestroyProjectile { get; set; }
            public Il2CppStringArray ignoreImmunityForBloonTypes { get; set; }
            public bool ignoreNonTargetables { get; set; }
            public bool blockSpawnChildren { get; set; }

            public Postfix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
            ref bool distributeToChildren, ref bool overrideDistributeBlocker, ref bool createEffect, ref Tower tower,
            ref bool canDestroyProjectile, ref Il2CppStringArray ignoreImmunityForBloonTypes, ref bool ignoreNonTargetable,
            ref bool blockSpawnChildren) : base("BloonDamagedEvent.Post")
            {
                this.bloon = __instance;
                this.damageTaken = totalAmount;
                this.damageTypes = types;
                this.projectile = projectile;
                this.distrubuteToChildren = distributeToChildren;
                this.overrideDistributeBlocker = overrideDistributeBlocker;
                this.createEffect = createEffect;
                this.tower = tower;
                this.canDestroyProjectile = canDestroyProjectile;
                this.ignoreImmunityForBloonTypes = ignoreImmunityForBloonTypes;
                this.ignoreNonTargetables = ignoreNonTargetable;
                this.blockSpawnChildren = blockSpawnChildren;
            }
        }
    }
}
