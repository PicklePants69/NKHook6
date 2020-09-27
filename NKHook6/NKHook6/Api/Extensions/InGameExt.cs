﻿using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Simulation.Simulation;

namespace NKHook6.Api.Extensions
{
    public static class InGameExt
    {
        public static double getCash(this InGame inGame)
        {
            try
            {
                CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
                return cashManager.cash.Value;
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
                return 0;
            }
        }
        public static void setCash(this InGame inGame, double newCash)
        {
            try
            {
                CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
                cashManager.cash.Value = newCash;
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        public static double getHealth(this InGame inGame)
        {
            try
            {
                return inGame.bridge.simulation.health.Value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
                return 0;
            }
        }
        public static void setHealth(this InGame inGame, double newHealth)
        {
            try
            {
                inGame.bridge.simulation.health.Value = newHealth;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
    }
}