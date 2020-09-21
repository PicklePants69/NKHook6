using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using UnhollowerBaseLib;

namespace NKHook6.Api.CustomTypes
{
    public class DamageInfo
    {
        public Bloon BloonInstance { get; set; }
        public float DamageTaken { get; set; }
        public Il2CppStringArray DamageTypes { get; set; }
        public Projectile Projectile { get; set; }
        public bool DistributeToChildren { get; set; }
        public bool OverrideDistributeBlocker { get; set; }
        public bool CreateEffect { get; set; }
        public Tower TowerInstance { get; set; }
        public bool CanDestroyProjectile { get; set; }
        public Il2CppStringArray IgnoreImmunityForBloonTypes { get; set; }
        public bool IgnoreNonTargetables { get; set; }
        public bool BlockSpawnChildren { get; set; }
    }
}
