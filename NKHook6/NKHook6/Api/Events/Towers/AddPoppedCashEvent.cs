using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class AddPoppedCashEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;
            public float cash;

            public Pre(ref Tower __instance, ref float cash) : base("Tower.AddPoppedCashEvent.Pre")
            {
                this.tower = __instance;
                this.cash = cash;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public float cash;

            public Post(ref Tower __instance, ref float cash) : base("Tower.AddPoppedCashEvent.Post")
            {
                this.tower = __instance;
                this.cash = cash;
            }
        }
    }
}
