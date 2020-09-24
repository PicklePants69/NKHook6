using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerUpdatedModelEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public Model model;
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance, ref Model modelToUse) : base("TowerUpdatedModelEvent.Pre")
            {
                this.tower = __instance;
                this.model = modelToUse;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public Model model;

            public Postfix(ref Tower __instance, ref Model modelToUse) : base("TowerUpdatedModelEvent.Post")
            {
                this.tower = __instance;
                this.model = modelToUse;
            }
        }
    }
}
