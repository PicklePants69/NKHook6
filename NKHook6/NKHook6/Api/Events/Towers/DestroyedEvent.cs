using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class DestroyedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;

            public Pre(ref Tower __instance) : base("Tower.DestroyedEvent.Pre")
            {
                this.tower = __instance;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;

            public Post(ref Tower __instance) : base("Tower.DestroyedEvent.Post")
            {
                this.tower = __instance;
            }
        }
    }
}
