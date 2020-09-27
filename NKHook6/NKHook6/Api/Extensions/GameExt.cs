using Assets.Scripts.Unity;
using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class GameExt
    {
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
