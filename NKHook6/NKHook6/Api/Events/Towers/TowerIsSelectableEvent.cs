using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerIsSelectableEvent
    {
        public class Prefix : EventBaseCancellable
        {
            public Tower tower;

            public Prefix(ref Tower __instance) : base("TowerIsSelectableEvent.Pre")
            {
                this.tower = __instance;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public bool result;
            public Postfix(ref Tower __instance, ref bool __result) : base("TowerIsSelectableEvent.Post")
            {
                this.tower = __instance;
                this.result = __result;
            }
        }
    }
}
