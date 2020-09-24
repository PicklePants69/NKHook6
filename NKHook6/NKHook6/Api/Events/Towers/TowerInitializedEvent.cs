using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Objects;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerInitializedEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public Entity target;
            public Model model;
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("TowerInitializedEvent.Pre")
            {
                this.tower = __instance;
                this.target = target;
                this.model = modelToUse;
            }
        }
        public class Postfix : EventBase
        {
            public Tower tower;
            public Entity target;
            public Model model;

            public Postfix(ref Tower __instance, ref Entity target, ref Model modelToUse) : base("TowerInitializedEvent.Post")
            {
                this.tower = __instance;
                this.target = target;
                this.model = modelToUse;
            }
        }
    }
}
