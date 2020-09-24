using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonSpawnedEvent
    {
        public class Prefix : EventBase
        {
            public Bloon bloon;
            public Entity target { get; set; }
            public Model model { get; set; }
            public bool replaceMethod { get; set; }

            public Prefix(ref Bloon __instance, ref Entity target, ref Model model) : base("BloonSpawnedEvent.Pre")
            {
                this.bloon = __instance;
                this.model = model;
                this.target = target;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;
            public Entity target { get; set; }
            public Model model { get; set; }

            public Postfix(ref Bloon __instance, ref Entity target, ref Model model) : base("BloonSpawnedEvent.Post")
            {
                this.bloon = __instance;
                this.model = model;
                this.target = target;
            }
        }
    }
}
