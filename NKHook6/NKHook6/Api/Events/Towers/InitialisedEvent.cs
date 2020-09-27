using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events._Towers
{
    public class InitialisedEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower instance;
            public Entity entity;
            public Model model;

            public Pre(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("Tower.InitialisedEvent.Pre")
            {
                this.instance = __instance;
                this.entity = target;
                this.model = modelToUse;
            }
        }
        public class Post : EventBase
        {
            public Tower instance;
            public Entity entity;
            public Model model;

            public Post(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("Tower.InitialisedEvent.Post")
            {
                this.instance = __instance;
                this.entity = target;
                this.model = modelToUse;
            }
        }
    }
}
