using Assets.Scripts.Models.Profile;
using Assets.Scripts.Simulation.Towers;

namespace NKHook6.Api.Events.Towers
{
    public class GetSaveDataEvent
    {
        public class Pre : EventBaseCancellable
        {
            public Tower tower;

            public Pre(ref Tower __instance) : base("Tower.GetSaveDataEvent.Pre")
            {
                this.tower = __instance;
            }
        }

        public class Post : EventBase
        {
            public Tower tower;
            public TowerSaveDataModel result;
            public Post(ref Tower __instance, ref TowerSaveDataModel __result) : base("Tower.GetSaveDataEvent.Post")
            {
                this.tower = __instance;
                this.result = __result;
            }
        }
    }
}
