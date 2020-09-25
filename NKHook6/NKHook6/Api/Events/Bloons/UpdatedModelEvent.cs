using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class UpdatedModelEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Bloon bloon;
            public Model model;

            public Pre(ref Bloon __instance, ref Model modelToUse) : base("Bloon.UpdatedModelEvent.Pre")
            {
                this.bloon = __instance;
                this.model = modelToUse;
            }
        }

        public class Post : EventBase
        {
            public Bloon bloon;
            public Model model;

            public Post(ref Bloon __instance, ref Model modelToUse) : base("Bloon.UpdatedModelEvent.Post")
            {
                this.bloon = __instance;
                this.model = modelToUse;
            }
        }
    }
}
