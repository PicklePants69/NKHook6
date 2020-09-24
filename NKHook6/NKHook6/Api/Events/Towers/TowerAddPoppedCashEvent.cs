using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerAddPoppedCashEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public float cash;
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance, ref float cash) : base("TowerAddPoppedCashEvent.Pre")
            {
                this.tower = __instance;
                this.cash = cash;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public float cash;

            public Postfix(ref Tower __instance, ref float cash) : base("TowerAddPoppedCashEvent.Post")
            {
                this.tower = __instance;
                this.cash = cash;
            }
        }
    }
}
