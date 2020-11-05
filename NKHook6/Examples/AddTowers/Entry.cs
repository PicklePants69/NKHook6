using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Simulation.Input;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.Upgrade;
using Harmony;
using MelonLoader;
using NKHook6;
using NKHook6.Api;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using NKHook6.Api.Towers;
using NKHook6.Api.Upgrades;
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

            //Build and register upgrades
            UpgradeModel customUpgrade = new UpgradeBuilder().SetName("CustomUpgrade").build();
            UpgradeRegistry.instance.register("CustomUpgrade", customUpgrade);
            UpgradePathModel upgradePathModel = new UpgradePathModel("CustomUpgrade", "CustomMonkey", 0, 0);
            game.getProfileModel().acquiredUpgrades.Add("CustomUpgrade");


            foreach (UpgradeModel upgrade in game.model.upgrades)
            {
                Logger.Log(upgrade.name);
            }


            //Build tower
            TowerModel customMonkey = new TowerBuilder()
                .SetName("CustomMonkey")
                .SetBaseId("CustomMonkey")
                .IgnoreBlockers(true)
                .SetRange(1000)
                .SetCost(20)
                .SetUpgrades(new UpgradePathModel[]{ upgradePathModel })
                .build(); //Create the model

            game.getProfileModel().unlockedTowers.Add("CustomMonkey"); //Unlock it so you can use it
            TowerRegistry.instance.register("CustomMonkey", customMonkey); //Register it

            TowerModel customMonkey100 = new TowerBuilder(customMonkey).SetName("CustomMonkey-100").build();
            game.getProfileModel().unlockedTowers.Add("CustomMonkey-100"); //Unlock it so you can use it
            TowerRegistry.instance.register("CustomMonkey-100", customMonkey100); //Register it




            /*TowerModel newModel = new TowerBuilder().SetName("Test").SetBaseId("Test").build();
            TowerRegistry.instance.register("Test", newModel);*/
        }
    }

	[HarmonyPatch(typeof(UpgradeScreen), "SelectUpgrade")]
	class UpgradeHook
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref UpgradeScreen __instance, UpgradeDetails details, bool showSelected = true)
		{
            Logger.Log(__instance.name);
            return true;
		}
	}
}
