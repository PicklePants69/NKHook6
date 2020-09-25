using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;

namespace NKHook6.Api.Events.Bloons
{
    public class SpawnedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Bloon bloon;
            public Entity target { get; set; }
            public Model model { get; set; }

            public Pre(ref Bloon __instance, ref Entity target, ref Model model) : base("Bloon.SpawnedEvent.Pre")
            {
                this.bloon = __instance;
                this.model = model;
                this.target = target;
            }
        }

        public class Post : EventBase
        {
            public Bloon bloon;
            public Entity target { get; set; }
            public Model model { get; set; }

            public Post(ref Bloon __instance, ref Entity target, ref Model model) : base("Bloon.SpawnedEvent.Post")
            {
                this.bloon = __instance;
                this.model = model;
                this.target = target;
            }
        }
    }
}
