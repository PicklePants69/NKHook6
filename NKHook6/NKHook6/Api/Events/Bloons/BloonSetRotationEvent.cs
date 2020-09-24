using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonSetRotationEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Bloon bloon;
            public float rotation;

            public Prefix(ref Bloon __instance, ref float rotation) : base("BloonSetRotationEvent.Pre")
            {
                this.bloon = __instance;
                this.rotation = rotation;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;
            public float rotation;

            public Postfix(ref Bloon __instance, ref float rotation) : base("BloonSetRotationEvent.Post")
            {
                this.bloon = __instance;
                this.rotation = rotation;
            }
        }
    }
}
