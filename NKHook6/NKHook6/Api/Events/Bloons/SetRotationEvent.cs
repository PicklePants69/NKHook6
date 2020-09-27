using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events._Bloons
{
    public partial class Bloons
    {
        public class SetRotationEvent
        {
            public class Pre : EventBaseCancellable
            {
                public Bloon instance;
                public float rotation;

                public Pre(ref Bloon __instance, ref float rotation) : base("Bloon.SetRotationEvent.Pre")
                {
                    this.instance = __instance;
                    this.rotation = rotation;
                }
            }

            public class Post : EventBase
            {
                public Bloon instance;
                public float rotation;

                public Post(ref Bloon __instance, ref float rotation) : base("Bloon.SetRotationEvent.Post")
                {
                    this.instance = __instance;
                    this.rotation = rotation;
                }
            }
        }
    }
}
