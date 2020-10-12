using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Unity;
using Harmony;
using MelonLoader;
using NKHook6;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Events._Towers;
using NKHook6.Api.Extensions;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;

namespace AddTowers
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.subscriber.register(typeof(Entry));
        }

        static TowerModel customModel = null;
        [EventAttribute("MainMenuLoadedEvent")]
        public static void onLoad(MainMenuEvents.LoadedEvent e)
        {
            Game game = Game.instance;

            
            /*Il2CppReferenceArray<TowerModel> towerList = game.model.towers;

            TowerModel[] modelArr = new TowerModel[towerList.Count+1];

            int x = 0;
            foreach(TowerModel model in towerList)
            {
                modelArr[x] = model;
                x++;
            }
            Il2CppReferenceArray<TowerModel> newList = new Il2CppReferenceArray<TowerModel>(modelArr);
            newList[x] = myModel;
*/
            /*game.model.towers[game.model.towers.Count] = myModel;
            */
            foreach (TowerModel model in game.model.towers)
            {
                if (model.name == "DartMonkey")
                {
                    customModel = model;
                    Logger.Log("Set default modded model");
                }
            }
            customModel.isGlobalRange = true;
            customModel.range = 9999;
        }

        [HarmonyPatch(typeof(TowerInventory), "Init")] // this method tells the game to create buttons for a given list of towers, allTowersInTheGame, which we modify here
        internal class TowerInit_Patch
        {
            [HarmonyPrefix]
            internal static bool Prefix(TowerInventory __instance, ref List<TowerDetailsModel> allTowersInTheGame)
            {
                allTowersInTheGame.Insert(allTowersInTheGame.Count, new TowerDetailsModel("DartMonkey", allTowersInTheGame.Count, 1, new Il2CppReferenceArray<ApplyModModel>(0)));
                return true;
            }
        }

        [EventAttribute("TowerCreatedEvent")]
        public static void onCreated(TowerEvents.CreatedEvent e)
        {
            if (e.tower.towerModel.name == "CustomMonkey")
            {
                //if(e.tower.towerModel.)
                //e.tower.towerModel = customModel;
            }
        }
    }
}
