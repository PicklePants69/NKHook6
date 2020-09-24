using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Bloons;

namespace NKHook6.Api.Events.Bloons
{
    public class BloonUpdatedModelEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Bloon bloon;
            public Model model;

            public Prefix(ref Bloon __instance, ref Model modelToUse) : base("BloonUpdatedModelEvent.Pre")
            {
                this.bloon = __instance;
                this.model = modelToUse;
            }
        }

        public class Postfix : EventBase
        {
            public Bloon bloon;
            public Model model;

            public Postfix(ref Bloon __instance, ref Model modelToUse) : base("BloonUpdatedModelEvent.Post")
            {
                this.bloon = __instance;
                this.model = modelToUse;
            }
        }
    }
}
