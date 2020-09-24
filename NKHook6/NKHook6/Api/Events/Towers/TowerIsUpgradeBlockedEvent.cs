using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerIsUpgradeBlockedEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public int path;
            public int tier;
            
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance, ref int path, ref int tier) : base("TowerIsUpgradeBlockedEvent.Pre")
            {
                this.tower = __instance;
                this.path = path;
                this.tier = tier;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public int path;
            public int tier;
            public string reason;
            public bool result;

            public Postfix(ref Tower __instance, ref int path, ref int tier, ref string reason, ref bool __result) 
                : base("TowerIsUpgradeBlockedEvent.Post")
            {
                this.tower = __instance;
                this.path = path;
                this.tier = tier;
                this.reason = reason;
                this.result = __result;
            }
        }
    }
}
