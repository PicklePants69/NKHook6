using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using NKHook6;
using NKHook6.Api.Events;
using NKHook6.Api.Events._InGame;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Events._Simulation;
using NKHook6.Api.Extensions;
using NKHook6.Api.Towers;
using NKHook6.Api.Towers.Behaviors;
using NKHook6.Api.Upgrades;
using System.Collections.Generic;

namespace UltraQuincy
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.instance.listen(this.GetType());
        }

        [EventAttribute("MainMenuLoadedEvent")]
        public static void onMenu(MainMenuEvents.LoadedEvent e)
        {
            
            OrbitModel newOrbit = new OrbitModel(Game.instance.getTowerModel("BoomerangMonkey", 5, 2, 0).getBehaviorByType("OrbitModel").Clone().Pointer);
            AttackModel newOrbitAttack = new AttackModel(Game.instance.getTowerModel("BoomerangMonkey", 5, 2, 0).getBehaviorByTypeAndName("AttackModel", "OrbitAttack").Clone().Pointer);
            AttackModel adoraAttack = new AttackModel(Game.instance.getTowerModel("Adora").getBehaviorByType("AttackModel").Clone().Pointer);
            AttackBuilder newAttack = new AttackBuilder("Quincy")
                .AddWeapon(adoraAttack.weapons[0])
                .ForEachWeapon(weapon =>
                {
                    weapon.Rate = .01f;
                });
            newOrbit.name += "0_";
            TowerBuilder ultraQuincy = new TowerBuilder("Quincy")
                .SetName("UltraQuincy")
                .SetBaseId("UltraQuincy")
                .SetAnimationSpeed(0.5f)
                .SetDontDisplayUpgrades(true)
                .SetUpgrades(new UpgradePathModel[] { })
                .AddBehavior(newAttack.build())
                .AddBehavior(newOrbit)
                .AddBehavior(newOrbitAttack)
                .SetVisibleInShop(true);

            TowerRegistry.instance.register("UltraQuincy", ultraQuincy);
            Game.instance.getProfileModel().primaryHero = "UltraQuincy";
            Game.instance.getProfileModel().unlockedTowers.Add("UltraQuincy");
            Game.instance.getProfileModel().unlockedHeroes.Add("UltraQuincy");
        }
        [EventAttribute("StartMatchEvent")]
        public static void onStart(InGameEvents.StartMatchEvent e)
        {
            InGame.instance.bridge.SetHero("UltraQuincy");
        }
        static int roundOff = 1;
        [EventAttribute("RoundStartEvent")]
        public static void onRound(SimulationEvents.RoundStartEvent e)
        {
            Logger.Log("On Round");
            OrbitModel newOrbit = new OrbitModel(Game.instance.getTowerModel("BoomerangMonkey", 5, 2, 0).getBehaviorByType("OrbitModel").Clone().Pointer);
            AttackModel newOrbitAttack = new AttackModel(Game.instance.getTowerModel("BoomerangMonkey", 5, 2, 0).getBehaviorByTypeAndName("AttackModel", "OrbitAttack").Clone().Pointer);
            newOrbit.name += roundOff+"_";
            TowerBuilder updatedModel = TowerRegistry.instance.getItem("UltraQuincy").AddBehavior(newOrbit).AddBehavior(newOrbitAttack);
            Il2CppSystem.Collections.Generic.List<TowerToSimulation> towers = InGame.instance.getTowers();
            foreach(TowerToSimulation tower in towers)
            {
                if (tower.tower.model.name.StartsWith("UltraQuincy"))
                {
                    tower.tower.towerModel = updatedModel.build();
                    Logger.Log("Updated the model");
                }
            }
            roundOff++;
        }
    }
}
