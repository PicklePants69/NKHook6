using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class UpdatedModelEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;
            public Model model;

            public Pre(ref Tower __instance, ref Model modelToUse) : base("Tower.UpdatedModelEvent.Pre")
            {
                this.tower = __instance;
                this.model = modelToUse;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public Model model;

            public Post(ref Tower __instance, ref Model modelToUse) : base("Tower.UpdatedModelEvent.Post")
            {
                this.tower = __instance;
                this.model = modelToUse;
            }
        }
    }
}
