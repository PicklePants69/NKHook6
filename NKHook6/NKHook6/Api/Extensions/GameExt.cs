using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Player;
using NKHook6.Patches._ProfileModel;
using NKHook6.Patches.Bloon6Player;
using System.Collections.Generic;
using System.Linq;

namespace NKHook6.Api.Extensions
{
    public static class GameExt
    {
        public static ProfileModel getProfileModel(this Game game) => ProfilePatch.theModel;
        public static Btd6Player getBtd6Player(this Game game) => Bloon6PlayerPatch.thePlayer;

        public static double getMonkeyMoney(this Game game)
        {
            if(game != null)
            {
                return game.playerService.Player.Data.monkeyMoney.Value;
            }
            else
            {
                return 0;
            }
        }
        public static void setMonkeyMoney(this Game game, double newMM)
        {
            if(game != null)
            {
                game.playerService.Player.Data.monkeyMoney.Value = newMM;
            }
        }

        public static BloonModel getBloonModel(this Game game, string bloonName)
        {
            return game.model.GetBloon(bloonName);
        }
        public static List<BloonModel> getAllBloonModels(this Game game)
        {
            return game.model.bloons.ToList<BloonModel>();
        }
        
        public static TowerModel getTowerModel(this Game game, string towerName)
        {
            return game.model.GetTower(towerName);
        }
        public static List<TowerModel> getAllTowerModels(this Game game)
        {
            return game.model.towers.ToList<TowerModel>();
        }

        public static UpgradeModel getUpgradeModel(this Game game, string upgradeName)
        {
            return game.model.GetUpgrade(upgradeName);
        }
    }
}
