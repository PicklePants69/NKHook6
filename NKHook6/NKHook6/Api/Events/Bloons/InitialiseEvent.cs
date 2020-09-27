using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Objects;

namespace NKHook6.Api.Events._Bloons
{
    public partial class Bloons
    {
        public class InitialiseEvent
        {
            public class Pre : EventBaseCancellable
            {
                public Bloon instance;
                public Entity entity { get; set; }
                public Model model { get; set; }

                public Pre(ref Bloon __instance, ref Entity target, ref Model model) : base("Bloon.InitialiseEvent.Pre")
                {
                    this.instance = __instance;
                    this.model = model;
                    this.entity = target;
                }
            }

            public class Post : EventBase
            {
                public Bloon instance;
                public Entity entity { get; set; }
                public Model model { get; set; }

                public Post(ref Bloon __instance, ref Entity target, ref Model model) : base("Bloon.InitialiseEvent.Post")
                {
                    this.instance = __instance;
                    this.model = model;
                    this.entity = target;
                }
            }
        }
    }
}
