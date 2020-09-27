using Assets.Scripts.Unity.UI_New.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class InGameExt
    {
        public static double getCash(this InGame inGame)
        {
            return inGame.bridge.GetCash();
        }
        public static void setCash(this InGame inGame, double newCash)
        {
            inGame.bridge.SetCash(newCash);
        }
    }
}
