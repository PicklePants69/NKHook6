using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Unity;
using Harmony;
using NKHook6.Api.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;

namespace NKHook6.Api
{
    /*
     * Huge thanks to Kosmic (https://github.com/KosmicShovel) for helping research & actually add new tower content into the game
     */
    public class TowerRegistry : Registry<TowerModel>
    {
        public static TowerRegistry instance;
        internal TowerRegistry()
        {
            instance = this;
        }

        public override void register(string ID, TowerModel item)
        {
            base.register(ID, item);

            Game game = Game.instance;
            var p_towerList = game.model.towers;
            List<TowerModel> towerList = new List<TowerModel>(p_towerList.ToArray());
            towerList.Add(item);
            Il2CppReferenceArray<TowerModel> m_towerList = new Il2CppReferenceArray<TowerModel>(towerList.ToArray());
            game.model.towers = m_towerList;
        }

        /* Necessary Patches */
        [HarmonyPatch(typeof(TowerInventory), "Init")]
        class InitPatch
        {
            [HarmonyPrefix]
            internal static bool Prefix(TowerInventory __instance, ref Il2CppSystem.Collections.Generic.List<ShopTowerDetailsModel> allTowersInTheGame)
            {
                Logger.Log("Adding tower to list...");
                TowerModel[] customTowerModels = instance.getItems();
                foreach(TowerModel customModel in customTowerModels)
                {
                    bool customPresent = false;
                    foreach(ShopTowerDetailsModel details in allTowersInTheGame)
                    {
                        if (details.name == customModel.name)
                        {
                            customPresent = true;
                        }
                    }
                    if (!customPresent)
                    {
                        Logger.Log("Added " + customModel.name);
                        allTowersInTheGame.Add(customModel.getShopDetails());
                    }
                }
                Logger.Log("Returning to game execution...");
                return true;
            }
        }
    }
}
