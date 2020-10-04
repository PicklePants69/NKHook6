using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using Harmony;
using NKHook6.Api.Enums;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Assets.Scripts.Models.Profile;

namespace NKHook6.Api.Utilities
{
    public class TowerUtils
    {
        /// <summary>
        /// A list of all towers currently on the map. Updated when a tower is initialised or destroyed
        /// </summary>
        public static List<Tower> TowersOnMap = new List<Tower>();

        public static List<TowerDetailsModel> AllTowersInTheGame = new List<TowerDetailsModel>();

        internal static Dictionary<ShopTowerDetailsModel, int> AddTheseTowersToList;

        public static TowerInventory TowerInventory;



        public static TowerModel GetTower(TowerType baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => GetTower(baseId.ToString(), tier1, tier2, tier3);

        public static TowerModel GetTower(string baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3) 
            => Game.instance.model.GetTower(baseId, tier1, tier2, tier3);



        //Thanks to BowDown for creating the AddTowerToInventory methods
        public static void AddToTowerSelection(ShopTowerDetailsModel details, TowerType insertBefore)
            => AddToTowerSelection(details, insertBefore.ToString(), AllTowersInTheGame);

        public static void AddToTowerSelection(ShopTowerDetailsModel details, string insertBefore)
            => AddToTowerSelection(details, insertBefore, AllTowersInTheGame);

        public static void AddToTowerSelection(ShopTowerDetailsModel details, int index) =>
            AddToTowerSelection(details, index, AllTowersInTheGame);


        public static void AddToTowerSelection(ShopTowerDetailsModel details, TowerType insertBefore, List<TowerDetailsModel> allTowersInTheGame)
            => AddToTowerSelection(details, insertBefore.ToString(), allTowersInTheGame);

        public static void AddToTowerSelection(ShopTowerDetailsModel details, string insertBefore, List<TowerDetailsModel> allTowersInTheGame)
        {
            TowerDetailsModel towerAfter = allTowersInTheGame.ToArray().FirstOrDefault(tower => tower.towerId == insertBefore);
            AddToTowerSelection(details, allTowersInTheGame.IndexOf(towerAfter), allTowersInTheGame);
        }

       
        public static void AddToTowerSelection(ShopTowerDetailsModel details, int index, List<TowerDetailsModel> allTowersInTheGame)
        {
            if (TowerInventory == null)
            {
                if (AddTheseTowersToList == null)
                    AddTheseTowersToList = new Dictionary<ShopTowerDetailsModel, int>();

                AddTheseTowersToList.Add(details, index);
                return;
            }

            allTowersInTheGame.Insert(index, details);
        }
    }

    [HarmonyPatch(typeof(TowerInventory), "Init")] // this method tells the game to create buttons for a given list of towers, allTowersInTheGame, which we modify here
    internal class TowerInit_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix(TowerInventory __instance, ref List<TowerDetailsModel> allTowersInTheGame)
        {
            TowerUtils.AllTowersInTheGame = allTowersInTheGame;
            TowerUtils.TowerInventory = __instance;

            if (TowerUtils.AddTheseTowersToList == null || TowerUtils.AddTheseTowersToList.Count == 0)
                return true;

            foreach (var item in TowerUtils.AddTheseTowersToList)
            {
                if (!allTowersInTheGame.Contains(item.key))
                    TowerUtils.AddToTowerSelection(item.Key, item.Value, allTowersInTheGame);
            }

            return true;
        }
    }
}