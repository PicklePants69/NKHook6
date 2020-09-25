using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class SoldEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;
            public float sellAmount;

            public Pre(ref Tower __instance, ref float sellAmount) : base("Tower.SoldEvent.Pre")
            {
                this.tower = __instance;
                this.sellAmount = sellAmount;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public float sellAmount;

            public Post(ref Tower __instance, ref float sellAmount) : base("Tower.SoldEvent.Post")
            {
                this.tower = __instance;
                this.sellAmount = sellAmount;
            }
        }
    }
}
