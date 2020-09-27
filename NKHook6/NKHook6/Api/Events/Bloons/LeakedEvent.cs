using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events._Bloons
{
    public class LeakedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Bloon instance;

            public Pre(ref Bloon __instance) : base("Bloon.LeakedEvent.Pre")
            {
                this.instance = __instance;
            }
        }

        public class Post : EventBase
        {
            public Bloon instance;

            public Post(ref Bloon __instance) : base("Bloon.LeakedEvent.Post")
            {
                this.instance = __instance;
            }
        }
    }
}
