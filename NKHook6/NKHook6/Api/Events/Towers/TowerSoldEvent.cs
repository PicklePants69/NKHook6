using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerSoldEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Tower tower;
            public float sellAmount;

            public Prefix(ref Tower __instance, ref float sellAmount) : base("TowerSoldEvent.Pre")
            {
                this.tower = __instance;
                this.sellAmount = sellAmount;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public float sellAmount;

            public Postfix(ref Tower __instance, ref float sellAmount) : base("TowerSoldEvent.Post")
            {
                this.tower = __instance;
                this.sellAmount = sellAmount;
            }
        }
    }
}
