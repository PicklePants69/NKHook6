using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Models.TowerSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class TowerModelExt
    {
        public static TowerDetailsModel getDetails(this TowerModel model)
        {
            return new TowerDetailsModel(model.name, 1, -1, null);
        }
        public static ShopTowerDetailsModel getShopDetails(this TowerModel model)
        {
            return new ShopTowerDetailsModel(model.name, 1, 0,0,0, -1, new ApplyModModel[0]);
        }
        public static Model getBehaviorByType(this TowerModel model, string behaviorType)
        {
            foreach(Model behavior in model.behaviors)
            {
                if (behavior.name.StartsWith(behaviorType))
                {
                    return behavior;
                }
            }
            return null;
        }
        public static Model getBehaviorByName(this TowerModel model, string behaviorName)
        {
            foreach(Model behavior in model.behaviors)
            {
                string theName = behavior.name.Split('_')[1];
                if (theName==behaviorName)
                {
                    return behavior;
                }
            }
            return null;
        }
        public static Model getBehaviorByTypeAndName(this TowerModel model, string behaviorType, string behaviorName)
        {
            foreach(Model behavior in model.behaviors)
            {
                string theName = behavior.name.Split('_')[1];
                if (behavior.name.StartsWith(behaviorType) && theName == behaviorName)
                {
                    return behavior;
                }
            }
            return null;
        }
    }
}
