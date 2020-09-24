using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerPositionTowerEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public Vector2 newPosition;
            public bool updateThrowCache;
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance, ref Vector2 newPosition, ref bool updateThrowCache)
                : base("TowerPositionTower.Pre")
            {
                this.tower = __instance;
                this.newPosition = newPosition;
                this.updateThrowCache = updateThrowCache;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public Vector2 newPosition;
            public bool updateThrowCache;

            public Postfix(ref Tower __instance, ref Vector2 newPosition, ref bool updateThrowCache) 
                : base("TowerPositionTower.Post")
            {
                this.tower = __instance;
                this.newPosition = newPosition;
                this.updateThrowCache = updateThrowCache;
            }
        }
    }
}
