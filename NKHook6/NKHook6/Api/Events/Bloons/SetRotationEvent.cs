using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class SetRotationEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Bloon bloon;
            public float rotation;

            public Pre(ref Bloon __instance, ref float rotation) : base("Bloon.SetRotationEvent.Pre")
            {
                this.bloon = __instance;
                this.rotation = rotation;
            }
        }

        public class Post : EventBase
        {
            public Bloon bloon;
            public float rotation;

            public Post(ref Bloon __instance, ref float rotation) : base("Bloon.SetRotationEvent.Post")
            {
                this.bloon = __instance;
                this.rotation = rotation;
            }
        }
    }
}
