using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class PoppedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Bloon bloon;

            public Pre(ref Bloon __instance) : base("Bloon.PoppedEvent.Pre")
            {
                this.bloon = __instance;
            }
        }

        public class Post : EventBase
        {
            public Bloon bloon;

            public Post(ref Bloon __instance) : base("Bloon.PoppedEvent.Post")
            {
                this.bloon = __instance;
            }
        }
    }
}
