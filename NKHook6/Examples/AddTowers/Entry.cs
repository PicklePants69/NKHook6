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
            EventRegistry.instance.listen(typeof(Entry));
        }

        [EventAttribute("MainMenuLoadedEvent")]
        public static unsafe void onLoad(MainMenuEvents.LoadedEvent e)
        {
            //Get instance
            Game game = Game.instance;
            //Build tower
            TowerModel customMonkey = new TowerBuilder().SetName("CustomMonkey").SetBaseId("CustomMonkey").SetDontDisplayUpgrades(true).SetCost(20).build(); //Create the model
            game.getProfileModel().unlockedTowers.Add("CustomMonkey"); //Unlock it so you can use it
            TowerRegistry.instance.register("CustomMonkey", customMonkey); //Register it
        }
    }
}
