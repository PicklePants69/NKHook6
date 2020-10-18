using Assets.Scripts.Models.Towers;
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
            return new ShopTowerDetailsModel(model.name, 1, 0,0,0, -1, null);
        }
    }
}
