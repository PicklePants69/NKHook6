using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class DestroyedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower instance;

            public Pre(ref Tower __instance) : base("Tower.DestroyedEvent.Pre")
            {
                this.instance = __instance;
            }
        }

        public class Post : EventBase
        {
            public Tower instance;

            public Post(ref Tower __instance) : base("Tower.DestroyedEvent.Post")
            {
                this.instance = __instance;
            }
        }
    }
}
