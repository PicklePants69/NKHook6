using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class IsSelectableEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;

            public Pre(ref Tower __instance) : base("Tower.IsSelectableEvent.Pre")
            {
                this.tower = __instance;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public bool result;
            public Post(ref Tower __instance, ref bool __result) : base("Tower.IsSelectableEvent.Post")
            {
                this.tower = __instance;
                this.result = __result;
            }
        }
    }
}
