using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.Upgrade;
using Harmony;
using NKHook6.Api.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;

namespace NKHook6.Api.Towers
{
    /*
     * Huge thanks to Kosmic (https://github.com/KosmicShovel) for helping research & actually add new tower content into the game
     */
    public class TowerRegistry : Registry<TowerModel>
    {
        //The registry instance
        public static TowerRegistry instance;
        internal TowerRegistry()
        {
            instance = this;
        }

        /// <summary>
        /// Register a new tower model with a given ID
        /// </summary>
        /// <param name="ID">The ID/Name of the tower</param>
        /// <param name="item">The actual model to be registered</param>
        public override void register(string ID, TowerModel item)
        {
            //Store it using the base function
            base.register(ID, item);

            //Get game instance
            Game game = Game.instance;
            //Get tower list
            var p_towerList = game.model.towers;
            //Create a managed version of the tower list to modify it easily
            List<TowerModel> towerList = new List<TowerModel>(p_towerList.ToArray());
            //Add the model
            towerList.Add(item);
            //Make the list an il2cpp array again
            Il2CppReferenceArray<TowerModel> m_towerList = new Il2CppReferenceArray<TowerModel>(towerList.ToArray());
            //Set the list of towers
            game.model.towers = m_towerList;
        }

        /* Necessary Patches */
        [HarmonyPatch(typeof(TowerInventory), "Init")]
        class InitPatch
        {
            [HarmonyPrefix]
            internal static bool Prefix(TowerInventory __instance, ref Il2CppSystem.Collections.Generic.List<ShopTowerDetailsModel> allTowersInTheGame)
            {
                //Get & Loop all models
                TowerModel[] customTowerModels = instance.getItems();
                foreach(TowerModel customModel in customTowerModels)
                {
                    //Bool to check if the custom tower already exists
                    bool customPresent = false;
                    //loop through existing shop tower models
                    foreach(ShopTowerDetailsModel details in allTowersInTheGame)
                    {
                        //Check if it exists
                        if (details.name == customModel.name)
                        {
                            customPresent = true;
                            break;
                        }
                    }
                    //If it doesnt, add it
                    if (!customPresent)
                    {
                        //Add the tower model to the shop
                        allTowersInTheGame.Add(customModel.getShopDetails());
                    }
                }
                //Return true executes the rest of the game code
                return true;
            }
        }

        // Kosmic Funny
        [HarmonyPatch(typeof(UpgradeScreen), "UpdateUi")]
        public class AddShopDetails
        {
            [HarmonyPrefix]
            public static bool Prefix(ref UpgradeScreen __instance, ref string towerId, ref string upgradeID)
            {
                //Get all models
                TowerModel[] customTowerModels = instance.getItems();
                foreach (TowerModel customModel in customTowerModels)
                {
                    //if the tower id in the upgrade screen is a modded one, make it a dart monkey upgrade screen for safety
                    if (towerId.Contains(customModel.baseId))
                    {
                        //towerId = "DartMonkey";
                        __instance.Close();
                        return false;
                    }
                }
                //Return true executes the rest of the game code
                return true;
            }
        }
    }
}
