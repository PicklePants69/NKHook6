using Assets.Scripts.Models.Towers;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Unity;
using NKHook6.Api.Enums;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NKHook6.Api.Utilities
{
    public class TowerUtils
    {
        /// <summary>
        /// A list of all towers currently on the map. Updated when a tower is initialised or destroyed
        /// </summary>
        public static List<Tower> TowersOnMap = new List<Tower>();
        /*public static string GetTowerName(DefaultTowerIds baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => GetTower(baseId, tier1, tier2, tier3).name;

        public static TowerModel GetTower(DefaultTowerIds baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => GetTower(baseId.ToString(), tier1, tier2, tier3);*/

        public static TowerModel GetTower(TowerType baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => GetTower(baseId.ToString(), tier1, tier2, tier3);

        public static TowerModel GetTower(string baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
        {
            string towerName = baseId;
            /*if (baseId == DefaultTowerIds.BallOfLight__Tower.ToString())
                towerName = towerName.Replace("__", "-");*/

            return Game.instance.model.GetTower(towerName, tier1, tier2, tier3);
        }
    }
}