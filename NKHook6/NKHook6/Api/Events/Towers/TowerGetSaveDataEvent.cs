using Assets.Scripts.Models.Profile;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class TowerGetSaveDataEvent
    {
        public class Prefix : EventBase
        {
            public Tower tower;
            public bool replaceMethod { get; set; }

            public Prefix(ref Tower __instance) : base("TowerGetSaveDataEvent.Pre")
            {
                this.tower = __instance;
            }
        }

        public class Postfix : EventBase
        {
            public Tower tower;
            public TowerSaveDataModel result;
            public Postfix(ref Tower __instance, ref TowerSaveDataModel __result) : base("TowerGetSaveDataEvent.Post")
            {
                this.tower = __instance;
                this.result = __result;
            }
        }
    }
}
