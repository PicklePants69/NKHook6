using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class InitializedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;
            public Entity target;
            public Model model;

            public Pre(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("Tower.InitializedEvent.Pre")
            {
                this.tower = __instance;
                this.target = target;
                this.model = modelToUse;
            }
        }
        public class Post : EventBase
        {
            public Tower tower;
            public Entity target;
            public Model model;

            public Post(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("Tower.InitializedEvent.Post")
            {
                this.tower = __instance;
                this.target = target;
                this.model = modelToUse;
            }
        }
    }
}
