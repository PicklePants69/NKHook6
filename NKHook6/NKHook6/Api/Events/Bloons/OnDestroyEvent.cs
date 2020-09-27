using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events._Bloons
{
    public partial class Bloons
    {
        public class OnDestroyEvent
        {
            public class Pre : EventBaseCancellable
            {
                public Bloon instance;

                public Pre(ref Bloon __instance) : base("Bloon.OnDestroyEvent.Pre")
                {
                    this.instance = __instance;
                }
            }

            public class Post : EventBase
            {
                public Bloon instance;

                public Post(ref Bloon __instance) : base("Bloon.OnDestroyEvent.Post")
                {
                    this.instance = __instance;
                }
            }
        }
    }
}
