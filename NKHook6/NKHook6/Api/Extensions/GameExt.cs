using Assets.Scripts.Models.Profile;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Player;
using Mono.CSharp;
using NKHook6.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class GameExt
    {
        public static ProfileModel getProfileModel(this Game game) => ProfileUtils.profileModel;
        public static Btd6Player getBtd6Player(this Game game) => ProfileUtils.btd6Player;

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
    }
}
