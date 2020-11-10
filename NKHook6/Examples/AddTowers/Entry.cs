using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Models.Towers.Weapons;
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
using NKHook6.Api.Towers.Behaviors;
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

            //Build tower
            TowerBuilder customMonkey = new TowerBuilder()
                .SetName("CustomMonkey") //Give it a name
                .SetBaseId("CustomMonkey") //Give it a base ID
                .IgnoreBlockers(true) //Make it ignore blockers
                .SetRange(100) //Set its range
                .SetCost(20) //Set the cost
                .SetAnimationSpeed(10)
                .SetUpgrades(new UpgradePathModel[]{ upgradePathModel })
                .SetVisibleInShop(true); //Make sure it is present in the shop (don't do this for upgrade models)

            AttackBuilder customAttack = new AttackBuilder()
                .SetRange(100)
                .SetFramesBeforeRetarget(1)
                .SetAttackThroughWalls(true);
            /*Logger.Log("Custom attack: " + customAttack.name);
            Logger.Log("Custom attack model: " + customAttack.build().name);*/


            List<TowerBehaviorModel> behaviors = new List<TowerBehaviorModel>();
            foreach (TowerBehaviorModel model in customMonkey.behaviors)
            {
                if(model.name.StartsWith("AttackModel"))
                {
                    //AttackModel attackModel = new AttackModel(model.Clone().Pointer);
                    //attackModel.range = 100;
                    behaviors.Add(customAttack.build());
                    foreach(WeaponModel weapon in customAttack.weapons)
                    {
                        weapon.rate = 0.01f;
                        Logger.Log(weapon.name);
                    }
                    //Logger.Log("Patched attack model");
                    continue;
                }
                behaviors.Add(model);
                //Logger.Log(model.name);
            }
            customMonkey.SetBehaviors(behaviors);

            game.getProfileModel().unlockedTowers.Add("CustomMonkey"); //Unlock it so you can use it
            TowerRegistry.instance.register("CustomMonkey", customMonkey); //Register it




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
