using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Unity;
using Harmony;
using MelonLoader;
using NKHook6;
using NKHook6.Api;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;


/*
 * DO NOT USE THIS MOD AS A REFERENCE!
 * This is still a WIP and is a topic currently being researched. Once we know how to add a tower, and a large majority of features to one, you will be able to use this file as a guide for using the NKHAPI to do it. As for right now, we are merely using this as a testing ground and a way to show off discoveries to other NKHAPI developers.
 */
namespace AddTowers
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.subscriber.register(typeof(Entry));
        }

        static TowerModel customMonkey = null;
        [EventAttribute("MainMenuLoadedEvent")]
        public static unsafe void onLoad(MainMenuEvents.LoadedEvent e)
        {
            Game game = Game.instance;

            //Build tower
            customMonkey = new TowerBuilder().SetName("CustomMonkey").SetBaseId("CustomMonkey").SetCost(20).build();

            var p_towerList = game.model.towers;
            List<TowerModel> towerList = new List<TowerModel>(p_towerList.ToArray());
            towerList.Add(customMonkey);
            Il2CppReferenceArray<TowerModel> m_towerList = new Il2CppReferenceArray<TowerModel>(towerList.ToArray());
            game.model.towers = m_towerList;

            foreach(TowerModel model in game.model.towers)
            {
                Logger.Log(model.name);
            }
        }

        [HarmonyPatch(typeof(TowerInventory), "Init")]
        class InitPatch
        {
            [HarmonyPrefix]
            internal static bool Prefix(TowerInventory __instance, ref List<TowerDetailsModel> allTowersInTheGame)
            {
                bool customPresent = false;
                foreach(TowerDetailsModel details in allTowersInTheGame)
                {
                    if(details.towerId == customMonkey.baseId)
                    {
                        customPresent = true;
                    }
                }
                if (!customPresent)
                {
                    allTowersInTheGame.Add(customMonkey.getShopDetails());
                }
                return true;
            }
        }
    }
}
