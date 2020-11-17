using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using NKHook6.Api.Events;
using NKHook6.Api.Events._InGame;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using NKHook6.Api.Towers;
using NKHook6.Api.Towers.Behaviors;

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
            AttackBuilder newAttack = new AttackBuilder("Quincy")
                .ForEachWeapon(weapon =>
                {
                    weapon.Rate = 0.01f;
                });
            TowerBuilder ultraQuincy = new TowerBuilder("Quincy")
                .SetName("UltraQuincy")
                .SetBaseId("UltraQuincy")
                .SetAnimationSpeed(0.5f)
                .SetDontDisplayUpgrades(true)
                .SetUpgrades(new UpgradePathModel[] { })
                .AddBehavior(newAttack.build())
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
    }
}
