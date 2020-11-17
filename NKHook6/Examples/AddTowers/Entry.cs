using Assets.Scripts.Models;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using MelonLoader;
using Newtonsoft.Json;
using NKHook6;
using NKHook6.Api.Events;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using NKHook6.Api.Towers;
using NKHook6.Api.Towers.Behaviors;
using NKHook6.Api.Upgrades;
using System.Collections.Generic;
using System.IO;


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

        public class GenObj
        {
            public string BehaviorModel;

            public GenObj(string text)
            {
                this.BehaviorModel = text;
            }
        }

        [EventAttribute("MainMenuLoadedEvent")]
        public static unsafe void onLoad(MainMenuEvents.LoadedEvent e)
        {
            //Get instance
            Game game = Game.instance;

            //Build and register upgrades (WIP, DO NOT USE!!!!!)
            UpgradeModel customUpgrade = new UpgradeBuilder().SetName("CustomUpgrade").build();
            UpgradeRegistry.instance.register("CustomUpgrade", customUpgrade);
            UpgradePathModel upgradePathModel = new UpgradePathModel("CustomUpgrade", "CustomMonkey", 0, 0);
            game.getProfileModel().acquiredUpgrades.Add("CustomUpgrade");

            List<GenObj> modelNames = new List<GenObj>();
            foreach(TowerModel model in Game.instance.getAllTowerModels())
            {
                foreach(Model bmodel in model.behaviors)
                {
                    modelNames.Add(new GenObj(bmodel.name));
                }
            }
            string modelList = JsonConvert.SerializeObject(modelNames, Formatting.Indented);
            File.WriteAllText("BehaviorModels.json", modelList);

            AttackBuilder customAttack = new AttackBuilder()
                .SetRange(100) //Set the attack range
                .SetFramesBeforeRetarget(1) //Set the frames before the monkey will target the next bloon
                .ForEachWeapon((weapon) => //For every weapon in the attack (You can do this in your own foreach loop, but this is beneficial for if youre trying to edit more properties directly after)
                {
                    weapon.rate = 0.1f; //Set the fire rate (MS between shots)
                })
                .SetAttackThroughWalls(true); //Make it so the attack ignores walls;

            AttackBuilder customQuincyAttack = new AttackBuilder("Quincy")
                .SetRange(100)
                .SetFramesBeforeRetarget(1)
                .ForEachWeapon(weapon =>
                {
                    weapon.rate = 0.1f;
                })
                .SetAttackThroughWalls(true);


            //Build tower
            TowerBuilder customMonkey = new TowerBuilder()
                .SetName("CustomMonkey") //Give it a name
                .SetBaseId("CustomMonkey") //Give it a base ID
                .IgnoreBlockers(true) //Make it ignore blockers
                .SetRange(100) //Set its range
                .SetCost(20) //Set the cost
                .SetAnimationSpeed(10) //Make him drink coffee
                .SetUpgrades(new UpgradePathModel[] { upgradePathModel }) //Unfinished, seems to have no effect at the moment
                .SetVisibleInShop(true) //Make sure it is present in the shop (don't do this for upgrade models)
                .RemoveBehavior("AttackModel") //Remove an attack model behavior
                .AddBehavior(customQuincyAttack.build())
                .AddBehavior(customAttack.build()); //Add our own

            game.getProfileModel().unlockedTowers.Add("CustomMonkey"); //Unlock it so you can use it
            TowerRegistry.instance.register("CustomMonkey", customMonkey); //Register it
        }
    }
}
