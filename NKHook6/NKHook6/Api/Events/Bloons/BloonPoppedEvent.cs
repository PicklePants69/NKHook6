using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using UnhollowerBaseLib;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonPoppedEvent
    {
        public class Prefix : EventBase
        {
            public Bloon bloon;
            public bool replaceMethod { get; set; }

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
