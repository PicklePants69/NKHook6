using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonPoppedEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Bloon bloon;

            public Prefix(ref Bloon __instance) : base("BloonPoppedEvent.Pre")
            {
                this.bloon = __instance;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;

            public Postfix(ref Bloon __instance) : base("BloonPoppedEvent.Post")
            {
                this.bloon = __instance;
            }
        }
    }
}
