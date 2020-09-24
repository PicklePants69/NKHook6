using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonLeakedEvent
    {
        public class Prefix : EventBase
        {
            public Bloon bloon;
            public bool replaceMethod { get; set; }

            public Prefix(ref Bloon __instance) : base("BloonLeakedEvent.Pre")
            {
                this.bloon = __instance;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;

            public Postfix(ref Bloon __instance) : base("BloonLeakedEvent.Post")
            {
                this.bloon = __instance;
            }
        }
    }
}
