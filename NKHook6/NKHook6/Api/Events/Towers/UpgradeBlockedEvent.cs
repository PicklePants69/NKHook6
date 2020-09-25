using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class UpgradeBlockedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;
            public int path;
            public int tier;
            

            public Pre(ref Tower __instance, ref int path, ref int tier) : base("Tower.IsUpgradeBlockedEvent.Pre")
            {
                this.tower = __instance;
                this.path = path;
                this.tier = tier;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public int path;
            public int tier;
            public string reason;
            public bool result;

            public Post(ref Tower __instance, ref int path, ref int tier, ref string reason, ref bool __result) 
                : base("Tower.IsUpgradeBlockedEvent.Post")
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
